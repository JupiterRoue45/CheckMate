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
            builder.ToTable("ReservationRooms", table =>
            {
                table.HasCheckConstraint(
                    "CK_ReservationRooms_NumberOfAdults",
                    "[NumberOfAdults] > 0");

                table.HasCheckConstraint(
                    "CK_ReservationRooms_CheckInAfterRoomAttribution",
                    "[CheckedInAt] IS NULL OR [RoomId] IS NOT NULL");

                table.HasCheckConstraint(
                    "CK_ReservationRooms_CheckoutAfterCheckin",
                    "[CheckedOutAt] IS NULL OR " +
                    "([CheckedInAt] IS NOT NULL AND [CheckedOutAt] > [CheckedInAt])");
            });

            builder.HasKey(rr => rr.ReservationRoomId);

            builder.HasOne(rr => rr.Reservation)
                   .WithMany()
                   .HasForeignKey(rr => rr.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(rr => rr.RoomType)
                   .WithMany()
                   .HasForeignKey(rr => rr.RoomTypeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(rr => rr.Room)
                     .WithMany()
                     .HasForeignKey(rr => rr.RoomId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired(false);

            builder.Property(rr => rr.CheckedInAt)
                .IsRequired(false);

            builder.Property(rr => rr.CheckedOutAt)
                .IsRequired(false);

                 builder.Property(rr => rr.IsBlocked)
                     .HasDefaultValue(false)
                     .IsRequired();

            builder.Property(rr => rr.Comment)
                     .HasMaxLength(500)
                     .IsRequired(false);

            builder.Property(rr => rr.NumberOfAdults)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(rr => rr.NumberOfChildren)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(rr => rr.NumberOfInfants)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Ignore(rr => rr.IsOccupied);
        }
    }
}
