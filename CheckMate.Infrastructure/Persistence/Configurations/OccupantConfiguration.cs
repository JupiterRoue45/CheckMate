using CheckMate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.Configurations
{
    public class OccupantConfiguration : IEntityTypeConfiguration<Occupant>
    {
        public void Configure(EntityTypeBuilder<Occupant> builder)
        {
            builder.ToTable("Occupants");

            builder.HasKey(o => new { o.ReservationRoomId, o.PersonId });

            builder.HasOne(o => o.ReservationRoom)
                .WithMany()
                .HasForeignKey(o => o.ReservationRoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(o => o.Person)
                .WithMany()
                .HasForeignKey(o => o.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(o => o.StartingDate)
                .IsRequired();

            builder.Property(o => o.EndingDate)
                .IsRequired(false);
        }
    }
}
