using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Client;
using RoomReservation.Application.Services;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Utilities.Strategy.Booking;

public class GoogleBookingStrategy : IBookingStrategy
{
    private readonly ILogger _logger;
    private readonly IBookingRepository _bookingRepository;

    public GoogleBookingStrategy(
        IBookingRepository bookingRepository,
        ILogger<BookingService> logger)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
    }

    public string ServiceName => ExternalServices.Google;

    public void CreateBooking(Domain.Entities.Booking booking)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking)
    {
        _logger.LogInformation($"Getting bookings/reservations for client ID {booking.Id} using Google Service");
        var bookings = await _bookingRepository.GetAllBookingsByClientIdAsync(booking);
        return bookings;
    }
}
