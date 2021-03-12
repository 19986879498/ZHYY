using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBHIS.Models
{
    [Table("USERS")]
    public class Users
    {
        /// <summary>
        /// 服务id
        /// </summary>
        [Key,Column("USERID")]
        public int UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("PASSWORD")]
        public string  Password { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Column("EMPLNAME")]
        public string EmplName { get; set; }
        /// <summary>
        /// 客户端ID 唯一
        /// </summary>
       [Column("CLIENTID")]
        public string ClientId { get; set; }
        /// <summary>
        /// 服务角色
        /// </summary>
        [Column("ROLEID")]
        public string RoleID { get; set; }
        /// <summary>
        /// 服务秘钥
        /// </summary>
        [Column("USERSECRET")]
        public string UserSecret { get; set; }
        /// <summary>
        /// 可用服务
        /// </summary>
        [Column("SERVICENAMES")]
        public string ServiceNames { get; set; }
    }
}
