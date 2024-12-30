using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Clients;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Commands.CreateClient;

public class CreateClientCommandHandler(
        IClientRepository roomReservationRepository,
        ILogger<CreateClientCommandHandler> logger,
        IMapper mapper) : IRequestHandler<CreateClientCommand, int>
{
    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new client");
        var client = mapper.Map<Client>(request);

        var id = await roomReservationRepository.Create(client);
        return id;
    }
}
