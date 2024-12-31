
namespace RoomReservation.Application.Utilities.Strategy.Booking;

public class DefaultBookingStrategy : IBookingStrategy
{
    public string ServiceName => string.Empty;

    public void CreateBooking(Domain.Entities.Booking booking)
    {
        throw new NotImplementedException();
    }

    Task<List<Domain.Entities.Booking>> IBookingStrategy.GetBookings(DateTime rangeStart, DateTime rangeEnd)
    {
        throw new NotImplementedException();
    }
}
