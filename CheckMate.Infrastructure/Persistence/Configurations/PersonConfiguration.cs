using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons", table =>
            {
                table.HasCheckConstraint(
                    "CK_Persons_PhoneNumberRequireAreaCode",
                    "PhoneNumber IS NULL OR PhoneAreaCode IS NOT NULL");
            });

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.BirthDate)
                .IsRequired(false);

            builder.Property(p => p.PhoneAreaCode)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(p => p.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .IsRequired(false)
                .HasMaxLength(100);

        }
    }
}
