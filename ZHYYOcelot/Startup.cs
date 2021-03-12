using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace ZHYYOcelot
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //绑定鉴权中心
            //指定IdentityServer信息
            Action<IdentityServerAuthenticationOptions> action1 = o =>
            {
                o.Authority = "http://127.0.0.1:9000";
                o.ApiName = "RegisterApi";//要连接的应用名称
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "123321";//秘钥
            };
            Action<IdentityServerAuthenticationOptions> action2 = o =>
            {
                o.Authority = "http://127.0.0.1:9000";
                o.ApiName = "OutpatientApi";//要连接的应用名称
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
            };
            services.AddAuthentication()
            //对配置文件中使用chatKey进行配置
            .AddIdentityServerAuthentication("RegisterKey", action1)
            .AddIdentityServerAuthentication("OutpatientKey", action2);
            services.AddOcelot().AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseOcelot();
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
