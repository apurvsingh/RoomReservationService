using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Client;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Queries.GetClientById;

public class GetClientByIdQueryHandler(
        IClientRepository roomReservationRepository,
        ILogger<GetClientByIdQueryHandler> logger,
        IMapper mapper) : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting client by id {request.Id}");
        var client = await roomReservationRepository.GetClientByIdAsync(request.Id);
        var clientDto = mapper.Map<ClientDto>(client);
        return clientDto;
    }
}
