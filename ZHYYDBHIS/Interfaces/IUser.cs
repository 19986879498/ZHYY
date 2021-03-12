using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBHIS.Interfaces
{
   public  interface IUser
    {
        /// <summary>
        /// oracle 版本
        /// </summary>
        /// <returns></returns>
        //  public IEnumerable<ZHYYDBSqlServerHIS.Models.Authorize> GetAuthorizes();
        /// <summary>
        ///SqlServer版本
        /// </summary>
        public IEnumerable<ZHYYDBHIS.Models.Users> GetUsers();
    }
}
