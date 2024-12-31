namespace RoomReservation.Application.Utilities.Strategy.Booking;

public interface IBookingStrategy
{
    string ServiceName { get; }
    Task<List<Domain.Entities.Booking>> GetBookings(DateTime rangeStart, DateTime rangeEnd);
    void CreateBooking(Domain.Entities.Booking booking);
}
