using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class ReservationTypeConfiguration : IEntityTypeConfiguration<ReservationType>
    {
        public void Configure(EntityTypeBuilder<ReservationType> builder)
        {
            builder.ToTable("ReservationTypes");

            builder.HasKey(rt => rt.ReservationTypeId);

            builder.Property(rt => rt.ReservationTypeCode)
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(rt => rt.ReservationTypeCode)
                .IsUnique();

            builder.Property(rt => rt.Description)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(rt => rt.FreeCancellation)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
