using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Client;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;


// To-do: Remove this service
namespace RoomReservation.Application.Clients
{
    public interface IClientsService
    {
    }


    internal class ClientsService(IClientRepository roomReservationRepository,
        ILogger<ClientsService> logger,
        IMapper mapper) : IClientsService
    {

    }
}
