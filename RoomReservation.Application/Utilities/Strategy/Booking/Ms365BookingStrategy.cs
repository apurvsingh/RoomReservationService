using Microsoft.Extensions.Logging;
using RoomReservation.Common.RabbitMq;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Utilities.Strategy.Booking
{
    public class Ms365BookingStrategy : IBookingStrategy
    {
        private readonly ILogger _logger;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public Ms365BookingStrategy(
        IBookingRepository bookingRepository,
        ILogger<Ms365BookingStrategy> logger,
        IRabbitMqProducer rabbitMqProducer)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            _rabbitMqProducer = rabbitMqProducer;
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

        public void CreateBookingRabbitMq(string clientId, Domain.Entities.Booking bookingReq)
        {
            _logger.LogInformation("Creating a new booking using MS365 Service with RabbitMq");

            _rabbitMqProducer.SendBooking(bookingReq);
        }
    }
}
