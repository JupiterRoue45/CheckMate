using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class OngoingInvoiceConfiguration : IEntityTypeConfiguration<OngoingInvoice>
    {
        public void Configure(EntityTypeBuilder<OngoingInvoice> builder)
        {
            builder.ToTable("OngoingInvoices");

            builder.HasKey(oi => oi.OngoingInvoiceId);

            builder.HasOne(oi => oi.ReservationRoom)
                .WithMany(rr => rr.OngoingInvoices)
                .HasForeignKey(oi => oi.ReservationRoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(oi => oi.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne(oi => oi.Invoice)
                .WithMany(i => i.OngoingInvoices)
                .HasForeignKey(oi => oi.InvoiceNumber)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
