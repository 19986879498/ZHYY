using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZHYYOcelot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
      private static  IConfigurationRoot app = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {//配置consul中的配置文件
                  //  ZHYYSservicesCommon.ConfigCenter.ConsulConfigJson.ConfigRegist("", webBuilder);
                    webBuilder.UseStartup<Startup>().UseUrls($"http://*:{app["GateWayUrlPort"]}");
                })
            .ConfigureAppConfiguration((context)=> {
                context.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            });
    }
}
