using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using WifiFinder.Models;

using System.IO;

namespace WifiFinder.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WifiNetwork> WifiNetworks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WifiNetwork>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
