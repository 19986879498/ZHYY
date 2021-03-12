using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ZHYYDBSqlServerHIS;
using ZHYYDBSqlServerHIS.Models;
using ZHYYIdentityServer.IdserverConfig;
using ZHYYDBHIS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ZHYYIdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ZHYYDBSqlServerHIS.SqlSplit.OrclHelper helper = new ZHYYDBSqlServerHIS.SqlSplit.OrclHelper();
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
       }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        { //���������ַ���
            ZHYYDBSqlServerHIS.Startup s = new ZHYYDBSqlServerHIS.Startup(this.Configuration);
            s.ConfigureServices(services);
            //�����ڷ�����Services01�� Ҫ����IdentityServer4.AccessTokenValidation
            var idResources = new List<IdentityResource> {
                new  IdentityResources.OpenId(),//������ӣ�������Чscope����
                new IdentityResources.Profile()
            };
            ////��ȡDB���ݿ��е�����
            //IEnumerable<Users> userlist = new List<Users>();
            ////��scope
            ////var scope = services.BuildServiceProvider().CreateScope();
            ////DB context = scope.ServiceProvider.GetRequiredService<DB>();
            ////        userlist = context.Users;
            //����scope
            DB context = services.BuildServiceProvider().GetRequiredService<DB>();
            this.SqlSX(context);
            //��Ȩ����
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                     .AddResourceOwnerValidator<ValiedUser>()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients(context.Users));
            
                //.AddInMemoryIdentityResources(idResources)
                //.AddProfileService<Profile>();
            services.AddMvcCore();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentityServer();

        }
        public  void SqlSX(DB dB)
        {
            dB.Database.GetDbConnection().ConnectionString = helper.FZDHSqlStr(ZHYYDBSqlServerHIS.SqlSplit.OrclHelper.SqlExecType.Read, ZHYYDBSqlServerHIS.SqlSplit.OrclHelper.GetSqlMethod.FZ);
                /*this.Configuration.GetConnectionString("OracleDB");*/
        }
    }
}
