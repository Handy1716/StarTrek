using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Configurations
{
    public class PlanetConfiguration : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.ToTable("Planets");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Type)
                   .HasMaxLength(50);

            builder.HasMany(p => p.SpaceShips)
                   .WithOne(s => s.Planet)
                   .HasForeignKey(s => s.PlanetId);

            builder.HasMany(p => p.Missions)
                   .WithOne(m => m.TargetPlanet)
                   .HasForeignKey(m => m.TargetPlanetId);
        }
    }
}
