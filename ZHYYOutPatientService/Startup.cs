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

namespace ZHYYOutPatientService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //配置连接字符串
            ZHYYDBSqlServerHIS.Startup s = new ZHYYDBSqlServerHIS.Startup(this.Configuration);
            s.ConfigureServices(services);
            services.AddMvcCore();
            services.AddControllersWithViews();   
            //允许跨域
            //允许一个或多个来源可以跨域
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // 设定允许跨域的来源，有多个可以用','隔开
                    policy.WithOrigins("https://localhost:44362")//只允许https://localhost:5000来源允许跨域
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
        /// <summary>
        /// 获取Consul服务中的参数
        /// </summary>
        private static IConfigurationRoot root = new ConfigurationBuilder().AddJsonFile("consul.json").Build();
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

            //未封装前
            //ConsulConfig.ConsulRegist.RegistConsul().Wait();
            //    ConsulConfig.ConsulRegist.DesregisterConsul(lifetime);
            //封装后
            ZHYYSservicesCommon.ConsulConfig.ConsulRegist consul = new ZHYYSservicesCommon.ConsulConfig.ConsulRegist(root);
            consul.RegistConsul().Wait();
            consul.DesregisterConsul(lifetime);
        }
    }
}
