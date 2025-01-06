
using Microsoft.Extensions.Logging;
using RoomReservation.Common.RabbitMq;
using RoomReservation.Domain.Repositories;
using System.Diagnostics;

namespace RoomReservation.Application.Utilities.Strategy.Booking;

public class DefaultBookingStrategy : IBookingStrategy
{
    private readonly ILogger _logger;
    private readonly IBookingRepository _bookingRepository;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public DefaultBookingStrategy(
        IBookingRepository bookingRepository,
        ILogger<DefaultBookingStrategy> logger,
        IRabbitMqProducer rabbitMqProducer)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public string ServiceName => string.Empty;

    public async Task<int> CreateBooking(string clientId, Domain.Entities.Booking bookingReq)
    {
        _logger.LogInformation("Creating a new booking using Default Service");

        var id = await _bookingRepository.Create(bookingReq);
        return id;
    }

    public async Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking)
    {
        _logger.LogInformation($"Getting bookings/reservations for client ID {booking.ClientId} using Default Service");

        var bookings = await _bookingRepository.GetAllBookingsByClientIdAsync(booking);
        return bookings;
    }

    public void CreateBookingRabbitMq(string clientId, Domain.Entities.Booking bookingReq)
    {
        _logger.LogInformation("Creating a new booking using Default Service with RabbitMq");

        _rabbitMqProducer.SendBooking(bookingReq);
    }
}
