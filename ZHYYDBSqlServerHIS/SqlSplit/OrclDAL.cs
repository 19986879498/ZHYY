using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBSqlServerHIS.SqlSplit
{
    public class OrclDAL
    {
        //private static string WriteString = "";
        //private static string[] ReadStringMuti = null;
        ////分配权重的数组
        //private static int[] Score = new int[] { 6, 3, 1 };

        //private static List<string> ReadStringMutiWithScore = new List<string>();//带权重的数组

        //public OrclDAL()
        //{
        //    ///权重策略
        //    WriteString = System.Configuration.ConfigurationManager.AppSettings["WriteDB"];
        //    ReadStringMuti = System.Configuration.ConfigurationManager.AppSettings["ReadDBMuti"].Split('|');
        //    int index = 0;
        //    foreach (int count in Score)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            ReadStringMutiWithScore.Add(ReadStringMuti[index]);
        //        }
        //        index++;
        //    }
        //}

        //public static string GetConnectionString(ConnectionType connectionType)
        //{
        //    string conn = "";
        //    switch (connectionType)
        //    {
        //        case ConnectionType.Read:
        //            conn = Dispacher(); //ReadStringMuti[0];//暂时就拿第一个，待会儿再扩展负载均衡
        //            break;
        //        case ConnectionType.Write:
        //            conn = WriteString;
        //            break;
        //        default:
        //            break;
        //    }
        //    return conn;
        //}

        //private static int iNum = 0;//换成long
        ////负载均衡   自定义策略----轮询策略  权重策略：每个数据库得有个权重值，去不平均分配
        ////6:3:1 这个权重应该写在配置文件 每个字符串来一个，这里就写死
        //private static string Dispacher()
        //{
        //    //return ReadStringMuti[iNum++ % ReadStringMuti.Length];
        //    return ReadStringMutiWithScore[new Random(iNum++).Next(0, ReadStringMutiWithScore.Count)];

        //}
        //public enum ConnectionType
        //{
        //    Read,
        //    Write
        //}
    }
}
