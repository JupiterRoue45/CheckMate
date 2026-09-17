using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class InvoiceRowHistoryConfiguration : IEntityTypeConfiguration<InvoiceRowHistory>
    {
        public void Configure(EntityTypeBuilder<InvoiceRowHistory> builder)
        {
            builder.ToTable("InvoiceRowHistories", table =>
            {
                table.HasCheckConstraint(
                    "CK_InvoiceRowHistories_OriginalPrice",
                    "[OriginalPrice] > 0.00");

                table.HasCheckConstraint(
                    "CK_InvoiceRowHistories_NewPrice",
                    "[NewPrice] > 0.00");
            });

            builder.HasKey(ivh => ivh.InvoiceRowHistoryId);

            builder.HasOne(ivh => ivh.InvoiceRow)
                .WithMany()
                .HasForeignKey(ivh => ivh.InvoiceRowId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(ivh => ivh.CreationDateTime)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(ivh => ivh.OriginalPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(ivh => ivh.NewPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(ivh => ivh.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
