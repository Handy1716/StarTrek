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
    public class MissionConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.ToTable("Missions");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.StartDate)
                   .IsRequired();

            builder.Property(m => m.EndDate)
                   .IsRequired();

            builder.Property(m => m.Status)
                   .HasMaxLength(50);

            builder.HasOne(m => m.TargetPlanet)
                   .WithMany(p => p.Missions)
                   .HasForeignKey(m => m.TargetPlanetId);

            builder.HasOne(m => m.SpaceShip)
                   .WithMany(s => s.Missions)
                   .HasForeignKey(m => m.SpaceShipId);
        }
    }
}
