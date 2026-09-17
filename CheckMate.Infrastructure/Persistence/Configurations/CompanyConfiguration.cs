using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.Property(c => c.CompanyName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.TVA)
                .HasMaxLength(30)
                .IsRequired(false);

        }
    }
}
