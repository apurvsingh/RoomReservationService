using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Utilities.Strategy.Booking;

public interface IBookingStrategy
{
    string ServiceName { get; }
    Task<List<Domain.Entities.Booking>> GetBookings(Domain.Entities.Booking booking);
    void CreateBooking(Domain.Entities.Booking booking);
}
