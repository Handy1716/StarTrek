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
    public class SpaceShipConfiguration : IEntityTypeConfiguration<SpaceShip>
    {
        public void Configure(EntityTypeBuilder<SpaceShip> builder)
        {
            builder.ToTable("SpaceShips");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Type)
                   .HasMaxLength(50);

            builder.Property(s => s.Status)
                   .HasMaxLength(50);

            builder.HasMany(s => s.CrewMembers)
                   .WithOne(c => c.SpaceShip)
                   .HasForeignKey(c => c.SpaceShipId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Missions)
                   .WithOne(m => m.SpaceShip)
                   .HasForeignKey(m => m.SpaceShipId);
        }
    }
}
