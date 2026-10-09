using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class CancellationPolicyConfiguration : IEntityTypeConfiguration<CancellationPolicy>
    {
        public void Configure(EntityTypeBuilder<CancellationPolicy> builder)
        {
            builder.ToTable("CancellationPolicies", table =>
            {
                table.HasCheckConstraint(
                    "CK_CancellationPolicies_DeadlineHoursNullOrPositive",
                    "[DeadlineHours] IS NULL OR [DeadlineHours] > 0");
            });

            builder.HasKey(cp => cp.cancellationPolicyId);

            builder.Property(cp => cp.Code)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(cp => cp.Code)
                .IsUnique();

            builder.Property(cp => cp.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(cp => cp.DeadlineHours)
                .IsRequired(false);
        }
    }
}
