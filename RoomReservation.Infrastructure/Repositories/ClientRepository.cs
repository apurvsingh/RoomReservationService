using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Infrastructure.Persistence;

namespace RoomReservation.Infrastructure.Repositories
{
    internal class ClientRepository(RoomReservationsDbContext dbContext) : IClientRepository
    {
        public async Task<int> Create(Client entity)
        {
            dbContext.Clients.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clients = await dbContext.Clients.Include(c => c.Reservations).ToListAsync();
            return clients;
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            var client = await dbContext.Clients
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            return client;
        }
    }
}
