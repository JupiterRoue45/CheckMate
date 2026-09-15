using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.ToTable("ReservationRooms");

            builder.HasKey(rr => rr.ReservationRoomId);

            builder.HasOne(rr => rr.Reservation)
                   .WithMany()
                   .HasForeignKey(rr => rr.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(rr => rr.Room)
                     .WithMany()
                     .HasForeignKey(rr => rr.RoomId)
                     .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(rr => rr.RoomType)
                   .WithMany()
                   .HasForeignKey(rr => rr.RoomTypeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.Property(rr => rr.IsOccupied)
                   .IsRequired();

            builder.Property(rr => rr.IsBlocked)
                     .IsRequired()
                     .HasDefaultValue(false);

            builder.Property(rr => rr.Comment)
                     .HasMaxLength(500)
                     .IsUnicode(false)
                     .IsRequired(false);
        }
    }
}
