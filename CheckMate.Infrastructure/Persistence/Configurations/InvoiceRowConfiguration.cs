using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class InvoiceRowConfiguration : IEntityTypeConfiguration<InvoiceRow>
    {
        public void Configure(EntityTypeBuilder<InvoiceRow> builder)
        {
            builder.ToTable("InvoiceRows", table =>
            {
                table.HasCheckConstraint(
                    "CK_InvoiceRows_Quantity",
                    "[Quantity] > 0");

                table.HasCheckConstraint(
                    "CK_InvoiceRows_PositiveUnitPrice",
                    "[UnitPrice] > 0.00");
            });

            builder.HasKey(ir => ir.InvoiceRowId);

            builder.Property(ir => ir.CreationDateTime)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne(ir => ir.Service)
                .WithMany()
                .HasForeignKey(ir => ir.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(ir => ir.Quantity)
                .IsRequired();

            builder.HasOne(ir => ir.Room)
                .WithMany()
                .HasForeignKey(ir => ir.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(ir => ir.OngoingInvoice)
                .WithMany()
                .HasForeignKey(ir => ir.OngoingInvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(ir => ir.UnitPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
