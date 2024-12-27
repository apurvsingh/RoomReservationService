namespace RoomReservation.Application.Dtos.Reservation
{
    public class ReservationDto
    {
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public string? Title { get; set; }
        public string? RoomId { get; set; }
    }
}
