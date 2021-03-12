using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBSqlServerHIS.Models
{
    public class Users
    {
        /// <summary>
        /// 服务id
        /// </summary>
        [Key,Column("UserId")]
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("Password")]
        public string  Password { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Column("EmplName")]
        public string EmplName { get; set; }
        /// <summary>
        /// 客户端ID 唯一
        /// </summary>
       [Column("ClientId")]
        public string ClientId { get; set; }
        /// <summary>
        /// 服务角色
        /// </summary>
        [Column("RoleID")]
        public string RoleID { get; set; }
        /// <summary>
        /// 服务秘钥
        /// </summary>
        [Column("UserSecret")]
        public string UserSecret { get; set; }
        /// <summary>
        /// 可用服务
        /// </summary>
        [Column("ServiceNames")]
        public string ServiceNames { get; set; }
    }
}
