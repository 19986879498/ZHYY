using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZHYYDBSqlServerHIS.Models;
using ZHYYDBSqlServerHIS.Interfaces;
using ZHYYDBSqlServerHIS.Methods;
using ZHYYDBSqlServerHIS.SqlSplit;

namespace ZHYYIdentityServer.IdserverConfig
{

    public static class Config
    {
        /// <summary>
        /// 获取api资源
        /// </summary>
        /// <returns></returns>
        public static  IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("RegisterApi","挂号服务"),
                new ApiResource("OutpatientApi","门诊服务")
            };
        }
        /// <summary>
        /// 获取用户使用的客户端
        /// </summary>
        /// <returns></returns>
        public   static  IEnumerable <Client> GetClients(IEnumerable<Users> users)
        {
            return BindSqlServerClient(users);
        }
        /// <summary>
        /// 绑定sqlserver数据中的用户权限信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> BindSqlServerClient(IEnumerable<Users> userlist)
        {
            
            Console.WriteLine("sql的数据");
            Console.WriteLine(JsonConvert.SerializeObject(userlist));
            List<Client> clients = new List<Client>();
            foreach (Users item in userlist)
            {
                clients.Add(new Client()
                {
                    ClientId = item.ClientId,
                    ClientName = item.EmplName,
                    AllowedScopes = item.ServiceNames.Split(","),
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                       new Secret( item.UserSecret.Sha256())
                    }
                });
            }
            return clients;
        }
        /// <summary>
        /// 默认服务
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> DefaultClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId="hjw",//后期可当做权限认证
                    ClientSecrets={
                    new Secret("123456".Sha256())//后期可当做客户端访问服务的秘钥
                    },
                    AllowedScopes=new []{ "RegisterApi", "OutpatientApi"},//后期可做服务访问的限制
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword


                }
            };
        }
    }
}
