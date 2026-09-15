using CheckMate.Domain.Entities;
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
                    "CK_Reservations_ArrivalDate_DepartureDate",
                    "[ArrivalDate] < [DepartureDate]");

                table.HasCheckConstraint(
                    "CK_Reservations_NumberOfAdults",
                    "[NumberOfAdults] > 0");
            });

            builder.HasKey(r => r.ReservationId);

            builder.Property(r => r.ArrivalDate)
                .IsRequired();

            builder.Property(r => r.DepartureDate)
                .IsRequired();

            builder.Property(r => r.NumberOfAdults)
                .IsRequired();

            builder.Property(r => r.NumberOfChildren)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(r => r.NumberOfInfants)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(r => r.ReservationType)
                .WithMany()
                .HasForeignKey(r => r.ReservationTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(r => r.Booker)
                .WithMany()
                .HasForeignKey(r => r.BookerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
