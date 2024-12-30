using MediatR;
using RoomReservation.Application.Dtos.Booking;

namespace RoomReservation.Application.Commands.CreateClient;

public class CreateClientCommand : IRequest<int>
{
    public required string Name { get; set; }
    public IEnumerable<BookingDto>? Bookings { get; set; }
}
