using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBHIS.SqlSplit
{
    public class OrclHelper
    {
        public IConfigurationRoot root = new ConfigurationBuilder().AddJsonFile("ConnectStrings.json").Build();
        public string WriteStr = string.Empty;
        public string[] ReadStr = { };
        public string[] QZStr = { };
        public List<string> QZreadStr = new List<string>();
        public int num;
        public OrclHelper()
        {
            WriteStr = root["Write"].ToString() ;
            ReadStr = root["Read"].ToString().Split('|');
            QZStr = root["QZ"].ToString().Split('|');
            num = 0;
        }
       
        /// <summary>
        /// 负载均衡
        /// </summary>
        /// <returns></returns>
        public string GetFZRead()
        {
            if (num>ReadStr.Count())
            {
                num = 0;
            }
            string ReturnRes = ReadStr[new Random(num).Next(0, ReadStr.Count())];
            num++;
            return ReturnRes ;
        }
        /// <summary>
        /// 轮询
        /// </summary>
        /// <returns></returns>
        public string GetLXRead()
        {
            if (num > ReadStr.Count())
            {
                num = 0;
            }
            string ReturnRes = ReadStr[new Random(num%ReadStr.Count()).Next(0, ReadStr.Count())];
            num++;
            return ReturnRes;
        }
        /// <summary>
        /// 负载均衡
        /// </summary>
        /// <returns></returns>
        public string GetQZRead()
        {
            if (num> QZreadStr.Count())
            {
                num = 0;
            }
            for (int i = 0; i < ReadStr.Count(); i++)
            {
                for (int j  = 0; j < Convert.ToInt32(QZStr[i]); j++)
                {
                    QZreadStr.Add(ReadStr[i]);
                }
            }
            
            string ReturnRes = ReadStr[new Random(num).Next(0, QZreadStr.Count())];
            num++;
            return ReturnRes;
        }
        /// <summary>
        /// 读写的负载均衡
        /// </summary>
        /// <param name="sqlExec"></param>
        /// <returns></returns>
        public string FZDHSqlStr(SqlExecType sqlExec, GetSqlMethod sqlMethod)
        {
            switch (sqlExec)
            {
                case SqlExecType.Read:
                    switch (sqlMethod)
                    {
                        case GetSqlMethod.FZ://负载均衡
                            return GetFZRead();
                            break;
                        case GetSqlMethod.QZ://权重
                            GetQZRead();
                            break;
                        case GetSqlMethod.RoundRobin://轮询
                            GetLXRead();
                            break;
                        default:
                            return null;
                            break;
                    }

                        break;
                case SqlExecType.Write:
                    return WriteStr;
                    break;
                default:
                        return null;
                    break;
            }
            return null;
        }
        public enum SqlExecType
        {
            Write,//写
            Read//读
        }

        /// <summary>
        /// 选择集群的方式
        /// </summary>
        public enum GetSqlMethod
        {
            RoundRobin,//轮询
            FZ,//负载均衡
            QZ//权重
        }
    }
}
