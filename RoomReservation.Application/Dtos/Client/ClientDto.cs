using RoomReservation.Application.Dtos.Booking;

namespace RoomReservation.Application.Dtos.Client
{
    public class ClientDto
    {
        public required string Name { get; set; }
        public IEnumerable<BookingDto>? Bookings { get; set; }
    }
}
