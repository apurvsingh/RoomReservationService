using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Infrastructure.Persistence;

namespace RoomReservation.Infrastructure.Repositories;

internal class BookingRepository(RoomReservationsDbContext dbContext, ILogger<BookingRepository> logger) : IBookingRepository
{
    public async Task<int> Create(Booking request)
    {
        try
        {
            var isTimeSlotUnavailable = dbContext.Bookings.Where(b => b.RoomId == request.RoomId)
                .Any(b =>
                    (request.StartTime < b.StartTime && request.EndTime > b.StartTime) ||
                    (request.StartTime >= b.StartTime && request.StartTime < b.EndTime)
                );

            if (!isTimeSlotUnavailable) 
            {
                dbContext.Bookings.Add(request);
                await dbContext.SaveChangesAsync();
                return request.Id;
            }
        }

        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogInformation($"Concurrency conflict detected while trying to create booking : {ex.Message}");
            // Log the exception and handle conflict resolution
        }

        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return -1;
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

    public async Task<List<Booking>> GetAllBookingsByClientIdAsync(Booking booking)
    {
        var bookings = await dbContext.Bookings
            .Where(b => b.ClientId == booking.ClientId )
            //&&
            //    b.StartTime >= booking.StartTime && b.EndTime <= booking.EndTime)
            .ToListAsync();

        return bookings;
    }
}
