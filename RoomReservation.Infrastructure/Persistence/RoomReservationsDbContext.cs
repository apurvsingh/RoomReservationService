using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Infrastructure.Persistence;

internal class RoomReservationsDbContext : DbContext
{
    internal DbSet<Client> Clients { get; set; }
    internal DbSet<Booking> Bookings { get; set; }
    //internal DbSet<ExternalService> ExternalServices { get; set; }

    public RoomReservationsDbContext(DbContextOptions<RoomReservationsDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Client>()
        //    .HasMany(r => r.Bookings)
        //    .WithOne()
        //    .HasForeignKey(key => key.ClientId);

        // Define the one-to-many relationship between Client and Booking
        modelBuilder.Entity<Booking>()
            .HasOne<Client>(b => b.Client)        // Booking has one Client
            .WithMany(c => c.Bookings)           // Client has many Bookings
            .HasForeignKey(b => b.ClientId)      // Foreign key in Booking table
            .OnDelete(DeleteBehavior.Cascade);   // Cascade delete (optional)

        base.OnModelCreating(modelBuilder);
    }
}
