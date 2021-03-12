using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZHYYDBSqlServerHIS.Interfaces;
using ZHYYDBSqlServerHIS.SqlSplit;

namespace ZHYYDBSqlServerHIS.Methods
{
    public class User : IUser
    {
        public User(ZHYYDBSqlServerHIS.DB db)
        {
            this.db = db;
        }

        public ZHYYDBSqlServerHIS.DB db { get; }

        public OrclHelper helper = new OrclHelper();
        IEnumerable<ZHYYDBSqlServerHIS.Models.Users> IUser.GetUsers()
        {
            this.SqlSX(db);
            IEnumerable<ZHYYDBSqlServerHIS.Models.Users> aulist = this.db.Users;
            if (aulist == null)
            {
                return null;

            }
            return aulist;
        }

        public void SqlSX(DB dB)
        {
            string ConnectStr = helper.FZDHSqlStr(OrclHelper.SqlExecType.Read, OrclHelper.GetSqlMethod.FZ);
        Console.WriteLine(ConnectStr+DateTime.Now.ToString());
            dB.Database.GetDbConnection().ConnectionString = ConnectStr;
            /*this.Configuration.GetConnectionString("OracleDB");*/
        }
}
}
