
using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Utilities.Strategy.Booking;

public class DefaultBookingStrategy : IBookingStrategy
{
    private readonly ILogger _logger;
    private readonly IBookingRepository _bookingRepository;

    public DefaultBookingStrategy(
        IBookingRepository bookingRepository,
        ILogger<DefaultBookingStrategy> logger)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
    }

    public string ServiceName => string.Empty;

    public void CreateBooking(Domain.Entities.Booking booking)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking)
    {
        _logger.LogInformation($"Getting bookings/reservations for client ID {booking.Id} using Default Service");
        var bookings = await _bookingRepository.GetAllBookingsByClientIdAsync(booking);
        return bookings;
    }
}
