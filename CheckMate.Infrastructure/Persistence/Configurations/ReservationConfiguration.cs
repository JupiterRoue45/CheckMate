using CheckMate.Domain.Entities;
using CheckMate.Domain.Enums;
using CheckMate.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations", table =>
            {
                table.HasCheckConstraint(
                    "CK_Reservations_DepartureDateSupArrivalDate",
                    "[DepartureDate] > [ArrivalDate]");
            });

            builder.HasKey(r => r.ReservationId);

            builder.Property(r => r.ArrivalDate)
                .IsRequired();

            builder.Property(r => r.DepartureDate)
                .IsRequired();

            builder.Property(r => r.ReservationStatus)
                .HasDefaultValue(ReservationStatus.CONFIRMED)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(r => r.ReservationType)
                .WithMany()
                .HasForeignKey(r => r.ReservationTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(r => r.Booker)
                .WithMany()
                .HasForeignKey(r => r.BookerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

        }
    }
}
