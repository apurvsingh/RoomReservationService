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

        public async Task<int> CreateBooking(string clientId, Domain.Entities.Booking bookingReq)
        {
            _logger.LogInformation("Creating a new booking using Ms365 Service");

            var id = await _bookingRepository.Create(bookingReq);
            return id;
        }

        public async Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking)
        {
            _logger.LogInformation($"Getting bookings/reservations for client ID {booking.ClientId} using MS365 Service");

            var bookings = await _bookingRepository.GetAllBookingsByClientIdAsync(booking);
            return bookings;
        }
    }
}
