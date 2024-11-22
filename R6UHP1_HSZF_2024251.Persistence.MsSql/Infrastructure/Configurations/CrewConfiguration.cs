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
    public class CrewConfiguration : IEntityTypeConfiguration<Crew>
    {
        public void Configure(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("CrewMembers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Rank)
                   .HasMaxLength(50);

            builder.HasOne(c => c.SpaceShip)
                   .WithMany(s => s.CrewMembers)
                   .HasForeignKey(c => c.SpaceShipId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
