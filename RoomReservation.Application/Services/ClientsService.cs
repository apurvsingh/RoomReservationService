using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Client;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Clients
{
    public interface IClientsService
    {
        Task<int> CreateClient(CreateClientDto createClientDto);
        Task<IEnumerable<ClientDto>> GetAllClients();
        Task<ClientDto?> GetById(int id);
    }


    internal class ClientsService(IRoomReservationRepository roomReservationRepository,
        ILogger<ClientsService> logger,
        IMapper mapper) : IClientsService
    {
        public async Task<int> CreateClient(CreateClientDto createClientDto)
        {
            logger.LogInformation("Creating new client");
            var client = mapper.Map<Client>(createClientDto);

            var id = await roomReservationRepository.Create(client);
            return id;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClients()
        {
            logger.LogInformation("Getting all clients");
            
            var clients = await roomReservationRepository.GetAllClientsAsync();
            var clientsDto = mapper.Map<IEnumerable<ClientDto>>(clients);
            
            return clientsDto;
        }

        public async Task<ClientDto?> GetById(int id)
        {
            logger.LogInformation($"Getting client by id {id}");
            var client = await roomReservationRepository.GetClientByIdAsync(id);
            var clientDto = mapper.Map<ClientDto>(client);
            return clientDto;
        }
    }
}
