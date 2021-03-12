using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBHIS.Models
{
    [Table("AUTHORIZE")]
    public class Authorize
    {
        [Key]
        [Column("LOGIN_ID")]
        public string LoginID { get; set; }
        [Column("BEGIN_DATE")]
        public DateTime  BeginDate { get; set; }
        [Column("END_DATE")]
        public DateTime  EndDate { get; set; }
        [Column("USER_NAME")]
        public string  UserName { get; set; }
        [Column("OPERATE_TIME")]
        public string  OperateTime { get; set; }
        [Column("DEPT_NAME")]
        public string  DeptName { get; set; }

    }
}
