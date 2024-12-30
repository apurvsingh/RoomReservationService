using MediatR;
using RoomReservation.Application.Dtos.Reservation;

namespace RoomReservation.Application.Commands.CreateClient;

public class CreateClientCommand : IRequest<int>
{
    public required string Name { get; set; }
    public IEnumerable<ReservationDto>? Reservations { get; set; }
}
