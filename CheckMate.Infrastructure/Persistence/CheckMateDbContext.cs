using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace CheckMate.Infrastructure.Persistence
{
    public class CheckMateDbContext : IdentityDbContext<User, Role, string>
    {
        // DbSets
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<DailyService> DailyServices => Set<DailyService>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();
        public DbSet<InvoiceRowHistory> InvoiceRowHistories => Set<InvoiceRowHistory>();
        public DbSet<Occupant> Occupants => Set<Occupant>();
        public DbSet<OngoingInvoice> OngoingInvoices => Set<OngoingInvoice>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<ReservationRoom> ReservationRooms => Set<ReservationRoom>();
        public DbSet<ReservationType> ReservationTypes => Set<ReservationType>();
        public DbSet<Room > Rooms => Set<Room>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<RoomUnavailability> RoomUnavailabilities => Set<RoomUnavailability>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public CheckMateDbContext(DbContextOptions<CheckMateDbContext> options) : base(options) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CheckMateDbContext).Assembly);
        }
    }
}
