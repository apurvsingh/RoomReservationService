using RoomReservation.Application.Dtos.Reservation;

namespace RoomReservation.Application.Dtos.Client
{
    public class ClientDto
    {
        public required string Name { get; set; }
        public IEnumerable<ReservationDto>? Reservations { get; set; }
    }
}
