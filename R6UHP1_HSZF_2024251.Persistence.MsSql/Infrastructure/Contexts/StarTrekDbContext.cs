using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R6UHP1_HSZF_2024251.Model.Entities;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts
{

    public class StarTrekDbContext : DbContext
    {
        // DbSet-ek az entitásokhoz
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Mission> Missions { get; set; }

        // Konstruktor
        public StarTrekDbContext(bool resetDatabase = false)
        {
            if (resetDatabase)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureCreated();
            }
        }

        // Kapcsolat beállítása
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StarTrekDB;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
