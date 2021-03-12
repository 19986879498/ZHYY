using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZHYYDBHIS.Interfaces;

namespace ZHYYDBHIS.Methods
{
    public class User : IUser
    {
        public User(ZHYYDBHIS.DB db)
        {
            this.db = db;
        }

        public ZHYYDBHIS.DB db { get; }


        IEnumerable<ZHYYDBHIS.Models.Users> IUser.GetUsers()
        {
            IEnumerable<ZHYYDBHIS.Models.Users> aulist = this.db.USERS;
            if (aulist == null)
            {
                return null;

            }
            return aulist;
        }

        //public IEnumerable<Authorize> GetAuthorizes()
        //{
        //    IEnumerable<Authorize> aulist=this.db.AUTHORIZE;
        //    if (aulist==null)
        //    {
        //        return null;

        //    }
        //    return aulist;
        //}
    }
}
