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
        var bookings = await dbContext.Bookings.ToListAsync();
        return bookings;
    }

    public async Task<Booking?> GetReservationByTimeAsync(int id)
    {
        var bookings = await dbContext.Bookings
            .FirstOrDefaultAsync(r => r.Id == id);

        return bookings;
    }

    public Task<Booking?> GetReservationByTimeAsync(int id, Booking bookingRequestDto)
    {
        throw new NotImplementedException();
    }
}
