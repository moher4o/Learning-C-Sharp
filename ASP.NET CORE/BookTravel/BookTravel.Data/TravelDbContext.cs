using BookTravel.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookTravel.Data
{
    public class TravelDbContext : IdentityDbContext<User>
    {
        public DbSet<Airline> Airlines { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<TransferType> TransferTypes { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public TravelDbContext(DbContextOptions<TravelDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TransferType>()
                .HasMany(tt => tt.Transfers)
                .WithOne(t => t.TransferType)
                .HasForeignKey(t => t.TransferTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Airport>()
                .HasMany(a => a.TransferArrivals)
                .WithOne(t => t.ArrivalAirport)
                .HasForeignKey(t => t.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Airport>()
                .HasMany(a => a.TransferDepartures)
                .WithOne(t => t.DepartureAirport)
                .HasForeignKey(t => t.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Airline>()
                .HasMany(a => a.TransferArrivals)
                .WithOne(t => t.ArrivalAirline)
                .HasForeignKey(t => t.ArrivalAirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Airline>()
                .HasMany(a => a.TransferDepartures)
                .WithOne(t => t.DepartureAirline)
                .HasForeignKey(t => t.DepartureAirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Destination>()
                .HasMany(d => d.Transfers)
                .WithOne(t => t.Destination)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
