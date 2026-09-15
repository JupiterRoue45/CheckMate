using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class RoomUnavailabilityConfiguration : IEntityTypeConfiguration<RoomUnavailability>
    {
        public void Configure(EntityTypeBuilder<RoomUnavailability> builder)
        {
            builder.ToTable("RoomUnavailabilities");

            builder.HasKey(ru => ru.RoomUnavailabilityId);

            builder.HasOne(ru => ru.Room)
                .WithMany()
                .HasForeignKey(ru => ru.RoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(ru => ru.StartingDate)
                .IsRequired();

            builder.Property(ru => ru.EndingDate)
               .IsRequired();

            builder.Property(ru => ru.Reason)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(ru => ru.Comment)
                .IsRequired(false);

            builder.Property(ru => ru.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
        }
    }
}
