using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYDBSqlServerHIS
{
    public class DB:DbContext
    {
        public DB(DbContextOptions<DB> db):base(db)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Models.Users> Users { get; set; }
    }
}
