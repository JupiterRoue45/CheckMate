using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    internal class DailyServiceConfiguration : IEntityTypeConfiguration<DailyService>
    {
        public void Configure(EntityTypeBuilder<DailyService> builder)
        {
            builder.ToTable("DailyServices", table =>
            {
                table.HasCheckConstraint(
                    "CK_DailyServices_Quantity",
                    "[Quantity] > 0");

                table.HasCheckConstraint(
                    "CK_DailyServices_PositiveUnitPrice",
                    "[UnitPrice] > 0.00");
            });

            builder.HasKey(ds => new { ds.ReservationRoomId, ds.ServiceId });

            builder.HasOne(ds => ds.ReservationRoom)
                   .WithMany()
                   .HasForeignKey(ds => ds.ReservationRoomId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(ds => ds.Service)
                     .WithMany()
                     .HasForeignKey(ds => ds.ServiceId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();

            builder.Property(ds => ds.Quantity)
                     .IsRequired();

            builder.Property(ds => ds.UnitPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
