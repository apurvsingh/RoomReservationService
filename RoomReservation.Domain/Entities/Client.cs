using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? ExternalServiceId { get; set; }

        // Navigation Property: One-to-Many relationship
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
