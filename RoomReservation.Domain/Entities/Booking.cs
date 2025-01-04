using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities;

public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public string? Title { get; set; }
    public string? RoomId { get; set; }

    // Foreign Key
    public int ClientId { get; set; }

    // Navigation Property: Booking belongs to one Client
    public Client Client { get; set; } = null!;

    public string? ExternalService { get; set; }
}
