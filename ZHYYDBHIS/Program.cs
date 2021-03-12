using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZHYYDBHIS
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
        //    SqlSplit.OrclHelper helper = new SqlSplit.OrclHelper();
        //Console.WriteLine(helper.FZDHSqlStr(SqlSplit.OrclHelper.SqlExecType.Read, SqlSplit.OrclHelper.GetSqlMethod.FZ));
          CreateHostBuilder(args).Build().Run();
        }
        private IConfigurationRoot root = new ConfigurationBuilder().AddJsonFile("ConnectStrings.json").Build();
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {//配置consul中的配置文件
                    ZHYYSservicesCommon.ConfigCenter.ConsulConfigJson.ConfigRegist("ZHYYDBHIS/appsettings.json",webBuilder);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
