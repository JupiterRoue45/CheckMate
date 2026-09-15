using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(s => s.ServiceId);

            builder.Property(s => s.ServiceName)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(s => s.ServiceName)
                .IsUnique();

            builder.Property(s => s.ServiceDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(s => s.ServiceUnitPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(s => s.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
