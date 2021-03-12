using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZHYYDBSqlServerHIS.Interfaces;
using ZHYYDBSqlServerHIS.Models;
using Newtonsoft.Json;
using IdentityServer4.Models;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using ZHYYDBSqlServerHIS;
using Microsoft.EntityFrameworkCore;

namespace ZHYYIdentityServer.IdserverConfig
{
    public class ValiedUser : IResourceOwnerPasswordValidator
    {
        public ValiedUser(IUser user,IConfiguration configuration,DB db)
        {
            this.user = user;
            this.config = configuration;
            this.db = db;
        }
        

        public IUser user { get; }
        public IConfiguration config { get; }
        public DB db { get; }

        /// <summary>
        /// 内置User对象  可全局访问
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected  Users usersInfo { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
             SqlServerAuthentication(context);
           
        }
        /// <summary>
        /// SqlServer版本的身份认证
        /// </summary>
        /// <param name="context"></param>
        public void SqlServerAuthentication(ResourceOwnerPasswordValidationContext context)
        {
            this.SqlSX();
            Console.WriteLine(JsonConvert.SerializeObject(db.Users));
            IEnumerable<Users> autor = (from Users a in user.GetUsers() where a.UserName == context.UserName && a.Password == context.Password select a);
            if (autor.Count() == 0||autor==null)
            {
                Error(context);
            }
            Users a1 = autor.FirstOrDefault();
         
         
            Console.WriteLine(context.Password + "  " + context.UserName);
            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            if (context.UserName.Trim().ToUpper() == a1.UserName.Trim().ToUpper() && context.Password.Trim().ToUpper() == a1.Password.Trim().ToUpper())
            {//
                context.Result = new GrantValidationResult(
                    subject: context.UserName,//代表用户名
                    authenticationMethod: "custom",
                    claims: new Claim[] { new Claim("Name", context.UserName), new Claim("RealName", "胡锦伟"), new Claim("Email", "hjw@qq.com"),new Claim(JwtClaimTypes.Role,a1.RoleID) }//放入自己的信息
                    
                    );
            }
            else
            {
                Error(context);
            }
        }
        /// <summary>
        /// 认证失败返回
        /// </summary>
        /// <param name="context"></param>
        private void Error(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "验证失败");
        }
     
        public   void SqlSX()
        {
            db.Database.GetDbConnection().ConnectionString = config.GetConnectionString("OracleDB");
        }
        /// <summary>
        /// Oracle版本的身份认证
        /// </summary>
        /// <param name="context"></param>
        public void OracleAuthentication(ResourceOwnerPasswordValidationContext context)
        {
            //IEnumerable<Authorize> autor = (from Authorize a in user.GetAuthorizes() where a.UserName == context.UserName && a.LoginID == context.Password select a);
            //if (autor.Count() <= 0)
            //{
            //    Error(context);
            //}
            //Authorize a1 = autor.FirstOrDefault();
            //this.usersInfo = a1;
            //Console.WriteLine(JsonConvert.SerializeObject(a1) + " " + autor.Count());
            //Console.WriteLine(context.Password + "  " + context.UserName);
            ////根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            //if (context.UserName.Trim().ToUpper() == a1.UserName.Trim().ToUpper() && context.Password.Trim().ToUpper() == a1.LoginID.Trim().ToUpper())
            //{//
            //    context.Result = new GrantValidationResult(
            //        subject: context.UserName,//代表用户名
            //        authenticationMethod: "custom",
            //        claims: new Claim[] { new Claim("Name", context.UserName), new Claim("RealName", "胡锦伟"), new Claim("Email", "hjw@qq.com") }//放入自己的信息

            //        );
            //}
            //else
            //{
            //    Error(context);
            //}
        }
    }
}
