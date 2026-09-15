using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            builder.HasKey(pm => pm.PaymentMethodId);

            builder.Property(pm => pm.PaymentMethodCode)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(pm => pm.PaymentMethodCode)
                .IsUnique();

            builder.Property(pm => pm.PaymentMethodDescription)
                .HasMaxLength(200);

            builder.Property(pm => pm.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
        }
    }
}
