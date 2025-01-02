using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Utilities.Strategy.Booking
{
    public class Ms365BookingStrategy: IBookingStrategy
    {
        private readonly ILogger _logger;
        private readonly IBookingRepository _bookingRepository;

        public Ms365BookingStrategy(
        IBookingRepository bookingRepository,
        ILogger<Ms365BookingStrategy> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public string ServiceName => ExternalServices.Microsoft365;

        public void CreateBooking(Domain.Entities.Booking booking)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking)
        {
            _logger.LogInformation($"Getting bookings/reservations for client ID {booking.Id} using MS365 Service");
            var bookings = await _bookingRepository.GetAllBookingsByClientIdAsync(booking);
            return bookings;
        }
    }
}
