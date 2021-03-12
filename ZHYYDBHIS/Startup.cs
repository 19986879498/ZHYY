using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ZHYYDBHIS
{
    public class Startup
    {
        public Startup()
        {

        }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfigurationRoot root = new ConfigurationBuilder().AddJsonFile("ConnectStrings.json").Build();

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public SqlSplit.OrclHelper helper = new SqlSplit.OrclHelper();
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContextPool<ZHYYDBHIS. DB>(item => item.UseOracle(helper.FZDHSqlStr(SqlSplit.OrclHelper.SqlExecType.Read,SqlSplit.OrclHelper.GetSqlMethod.FZ)/*Configuration.GetConnectionString("OracleDB")*/, item => item.UseOracleSQLCompatibility("11")));
            services.AddTransient<Interfaces.IUser, Methods.User>();
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
                endpoints.MapGet("/", async context =>
                {
                    
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
