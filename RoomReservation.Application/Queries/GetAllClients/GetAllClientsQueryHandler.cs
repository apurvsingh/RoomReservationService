using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Client;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Queries.GetAllClients;

public class GetAllClientsQueryHandler(
        IClientRepository roomReservationRepository,
        ILogger<GetAllClientsQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
{
    public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all clients");

        var clients = await roomReservationRepository.GetAllClientsAsync();
        var clientsDto = mapper.Map<IEnumerable<ClientDto>>(clients);

        return clientsDto;
    }
}
