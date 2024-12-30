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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasMany(r => r.Bookings)
            .WithOne()
            .HasForeignKey(key => key.ClientId);

    }
}
