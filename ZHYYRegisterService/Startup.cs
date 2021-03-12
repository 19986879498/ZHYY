using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZHYYDBSqlServerHIS;

namespace ZHYYRegisterService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        /// <summary>
        /// ��ȡConsul�����еĲ���
        /// </summary>
        private static IConfigurationRoot root = new ConfigurationBuilder().AddJsonFile("consul.json").Build();
        
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
      {
            ZHYYDBSqlServerHIS.Startup s = new ZHYYDBSqlServerHIS.Startup(Configuration);
            s.ConfigureServices(services);
            services.AddMvcCore();
            services.AddControllersWithViews();
            //�������
            //����һ��������Դ���Կ���
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // �趨����������Դ���ж��������','����
                    policy.WithOrigins("https://localhost:44362")//ֻ����https://localhost:5000��Դ�������
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.AspNetCore.Hosting.IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CustomCorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ////����ע��
            //ConsulConfig.ConsulRegist.RegistConsul().Wait();
            ////����ע��
            //ConsulConfig.ConsulRegist.DesregisterConsul(lifetime);
            //����ע��
            ZHYYSservicesCommon.ConsulConfig.ConsulRegist consul = new ZHYYSservicesCommon.ConsulConfig.ConsulRegist(root);
            consul.RegistConsul().Wait();
            //����ע��
            consul.DesregisterConsul(lifetime);
        }
    }
}
