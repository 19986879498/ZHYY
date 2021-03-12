using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Winton.Extensions.Configuration.Consul;

namespace ZHYYSservicesCommon.ConfigCenter
{
   public  class ConsulConfigJson
    {
        /// <summary>
        /// 配置consul中的配置文件
        /// </summary>
        public static void ConfigRegist(string key,IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration((context, build) =>
            {
                build.AddConsul(string.IsNullOrEmpty(key)?"appsettings.json" : key, (soure) =>
                {
                    soure.ConsulConfigurationOptions = PZConsul;
                    //热加载  实时刷新consul中的配置中心
                    soure.ReloadOnChange = true;
                });
               
            });
        }
        public static void getbuild(IConfigurationBuilder builder,string key)
        {
            builder.AddConsul(string.IsNullOrEmpty(key) ? "appsettings.json" : key, (soure) =>
            {
                soure.ConsulConfigurationOptions = PZConsul;
                //热加载  实时刷新consul中的配置中心
                soure.ReloadOnChange = true;
            });
        }
        public static void PZConsul(ConsulClientConfiguration consul)
        {
            consul = new ConsulClientConfiguration() { 
            Address=new Uri("http://127.0.0.1:8500"),
            Datacenter="dc1"
            };

        }
    }
}
