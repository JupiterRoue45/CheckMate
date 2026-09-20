using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(c => c.CountryId);

            builder.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(c => c.CountryName)
                .IsUnique();

            builder.Property(c => c.PhoneDialCode)
                .IsRequired()
                .HasMaxLength(22);

            builder.HasData(CountrySeedData.GetCountries());
        }
    }
}
