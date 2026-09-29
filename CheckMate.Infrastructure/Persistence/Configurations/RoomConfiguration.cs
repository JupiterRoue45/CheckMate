using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.RoomNumber)
                   .IsRequired();
            builder.HasIndex(r => r.RoomNumber)
                   .IsUnique();

            builder.HasOne(r => r.RoomType)
                   .WithMany()
                   .HasForeignKey(r => r.RoomTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Floor).
                HasDefaultValue(null)
                .IsRequired(false);

            builder.Property(r => r.Area)
                .HasColumnType("decimal(6,2)")
                .HasDefaultValue(null)
                .IsRequired(false);

            builder.Property(r => r.RoomStatus)
                   .HasConversion<string>()
                   .IsRequired();

        }
    }
}
