namespace RoomReservation.Domain.Entities
{
    public class Client
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid ExternalServiceId { get; set; }

    }
}
