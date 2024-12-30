using MediatR;
using RoomReservation.Application.Dtos.Client;

namespace RoomReservation.Application.Queries.GetClientById;

public class GetClientByIdQuery(int id) : IRequest<ClientDto?>
{
    public int Id { get; } = id;
}
