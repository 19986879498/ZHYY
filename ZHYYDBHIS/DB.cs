using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZHYYDBHIS.Models;

namespace ZHYYDBHIS
{
    public class DB:DbContext
    {
        public DB(DbContextOptions<DB> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //模型类
        public DbSet<Authorize> AUTHORIZE { get; set; }
        //用户权限类
        public DbSet<Users> USERS { get; set; }
    }
}
