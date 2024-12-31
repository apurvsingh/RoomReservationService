using RoomReservation.Domain.Entities;

namespace RoomReservation.Application.Utilities.Strategy.Booking
{
    public class Ms365BookingStrategy : IBookingStrategy
    {
        public string ServiceName => ExternalServices.Microsoft365;

        public void CreateBooking(Domain.Entities.Booking booking)
        {
            throw new NotImplementedException();
        }

        Task<List<Domain.Entities.Booking>> IBookingStrategy.GetBookings(DateTime rangeStart, DateTime rangeEnd)
        {
            throw new NotImplementedException();
        }
    }
}
