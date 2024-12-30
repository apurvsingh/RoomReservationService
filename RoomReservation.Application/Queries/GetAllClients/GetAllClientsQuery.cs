using MediatR;
using RoomReservation.Application.Dtos.Client;

namespace RoomReservation.Application.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {

    }
}
