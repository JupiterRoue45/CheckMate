using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.AddressId);

            builder.Property(a => a.AddressNumber)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(a => a.StreetName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.AddressLabel)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(a => a.AddressLabel)
                .IsUnique();

            builder.Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.State)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.ZipCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
