using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZHYYDBSqlServerHIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {//配置consul中的配置文件
                 //   ZHYYSservicesCommon.ConfigCenter.ConsulConfigJson.ConfigRegist($"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace}/appsettings.json", webBuilder);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
