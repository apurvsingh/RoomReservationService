using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IRoomReservationRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<int> Create(Client entity);
    }
    
}
