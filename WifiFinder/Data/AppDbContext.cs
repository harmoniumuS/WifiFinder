using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using WifiFinder.Models;

namespace WifiFinder.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WifiNetwork> WifiNetworks {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=wifi.db"); 
        }
    }
}
