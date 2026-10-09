using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ComplementaryServiceConfiguration : IEntityTypeConfiguration<ComplementaryService>
    {
        public void Configure(EntityTypeBuilder<ComplementaryService> builder)
        {
            builder.ToTable("ComplementaryServices", table =>
            {
                table.HasCheckConstraint(
                    "CK_ComplementaryServices_PositiveQuantity",
                    "[Quantity] > 0");

                table.HasCheckConstraint(
                    "CK_ComplementaryServices_PositiveUnitPrice",
                    "[UnitPrice] > 0");

                table.HasCheckConstraint(
                    "CK_ComplementaryServices_StartingDateSupEndingDate",
                    "[StartingDate] > [EndingDate]");
            });

            builder.HasKey(cs => new {cs.ServiceId, cs.ReservationRoomId});

            builder.HasOne(cs => cs.Service)
                .WithMany()
                .HasForeignKey(cs => cs.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(cs => cs.ReservationRoom)
                .WithMany()
                .HasForeignKey(cs => cs.ReservationRoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(cs => cs.StartingDate)
                .IsRequired();

            builder.Property(cs => cs.EndingDate)
                .IsRequired();

            builder.Property(cs => cs.Quantity)
                .IsRequired();

            builder.Property(cs => cs.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(cs => cs.Reason)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(cs => cs.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(cs => cs.UserId)
                .IsRequired();
        }
    }
}
