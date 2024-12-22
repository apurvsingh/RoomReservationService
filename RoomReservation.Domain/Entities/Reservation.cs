namespace RoomReservation.Domain.Entities
{
    public class Reservation
    {
        public required Guid Id { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public string? Title { get; set; }
        public string? RoomId { get; set; }
        public required Guid ClientId { get; set; }
    }
}
