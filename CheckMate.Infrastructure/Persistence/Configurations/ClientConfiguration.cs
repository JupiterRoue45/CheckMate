using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.UseTptMappingStrategy();

            builder.ToTable("Clients", table =>
            {
                table.HasCheckConstraint(
                    "CK_Clients_PhoneNumberRequireAreaCode",
                    "PhoneNumber IS NULL OR PhoneAreaCode IS NOT NULL");
            });

            builder.HasKey(c => c.ClientId);

            builder.Property(c => c.CreationAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Property(c => c.PhoneAreaCode)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20) 
                .IsRequired(false);

            builder.Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired(false);
        }
    }
}
