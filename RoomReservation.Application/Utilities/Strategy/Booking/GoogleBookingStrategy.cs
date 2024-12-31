using RoomReservation.Domain.Entities;

namespace RoomReservation.Application.Utilities.Strategy.Booking;

public class GoogleBookingStrategy : IBookingStrategy
{
    public string ServiceName => ExternalServices.Google;

    public void CreateBooking(Domain.Entities.Booking booking)
    {
        throw new NotImplementedException();
    }

    Task<List<Domain.Entities.Booking>> IBookingStrategy.GetBookings(DateTime rangeStart, DateTime rangeEnd)
    {
        throw new NotImplementedException();
    }
}
