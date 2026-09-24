using CheckMate.Infrastructure.Identity.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .IsRequired();
            builder.HasIndex(rt => rt.Token)
                .IsUnique();

            builder.Property(rt => rt.JwtId)
                .IsRequired();

            builder.Property(rt => rt.Expires)
                .IsRequired();

            builder.Property(rt => rt.IsRevoked)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .IsRequired();

            builder.Property(rt => rt.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(rt => rt.RowVersion)
                .IsRowVersion();
        }
    }
}
