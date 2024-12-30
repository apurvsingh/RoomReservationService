using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Infrastructure.Persistence;

namespace RoomReservation.Infrastructure.Repositories;

internal class BookingRepository(RoomReservationsDbContext dbContext) : IBookingRepository
{
    public async Task<int> Create(Booking entity)
    {
        dbContext.Bookings.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Booking>> GetAllReservationsAsync()
    {
        var reservations = await dbContext.Bookings.ToListAsync();
        return reservations;
    }

    public async Task<Booking?> GetReservationByTimeAsync(int id)
    {
        var reservations = await dbContext.Bookings
            .FirstOrDefaultAsync(r => r.Id == id);

        return reservations;
    }
}
