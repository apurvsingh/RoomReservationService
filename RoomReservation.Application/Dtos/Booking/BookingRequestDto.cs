namespace RoomReservation.Application.Dtos.Booking;

public class BookingRequestDto
{
    public DateTime StartTime { get; set; }
    public  DateTime EndTime { get; set; }
    public string? Title { get; set; }
    public string? RoomId { get; set; }
    public string ExternalService { get; set; }
    public int ClientId { get; set; }
}
