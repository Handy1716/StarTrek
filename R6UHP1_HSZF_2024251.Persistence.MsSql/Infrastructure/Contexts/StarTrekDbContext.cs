using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Configurations;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Contexts
{

    public class StarTrekDbContext : DbContext
    {
        // DbSet-ek az entitásokhoz
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<Crew> CrewMembers { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Mission> Missions { get; set; }

        // Konstruktor az adatbázis opciók átadásához
        public StarTrekDbContext(DbContextOptions<StarTrekDbContext> options) : base(options)
        {
        }

        // Konfigurációk regisztrálása
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Az összes konfiguráció regisztrálása
            modelBuilder.ApplyConfiguration(new SpaceShipConfiguration());
            modelBuilder.ApplyConfiguration(new CrewConfiguration());
            modelBuilder.ApplyConfiguration(new PlanetConfiguration());
            modelBuilder.ApplyConfiguration(new MissionConfiguration());
        }
    }
}
