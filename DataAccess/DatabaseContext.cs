using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions dbContextOptions)
       : base(dbContextOptions)
        {
        }

        public DbSet<ClientSecrets> ClientSecrets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
