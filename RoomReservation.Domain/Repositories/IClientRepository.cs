using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<int> Create(Client entity);
    }
}
