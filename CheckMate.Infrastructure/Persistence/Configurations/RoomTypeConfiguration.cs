using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes", table =>
            {
                table.HasCheckConstraint(
                    "CK_Rank_SuperiorToZero",
                    "[Rank] > 0"
                );

                table.HasCheckConstraint(
                    "CK_MaxOccupancy_SuperiorToZero",
                    "[MaxOccupancy] > 0"
                );
            });

            builder.HasKey(rt => rt.RoomTypeId);

            builder.Property(rt => rt.RoomTypeName)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(rt => rt.RoomTypeName)
                .IsUnique();

            builder.Property(rt => rt.Description)
                .HasDefaultValue(null)
                .IsRequired(false);

            builder.Property(rt => rt.Rank)
                .IsRequired();

            builder.Property(rt => rt.MaxOccupancy)
                .IsRequired();
        }
    }
}
