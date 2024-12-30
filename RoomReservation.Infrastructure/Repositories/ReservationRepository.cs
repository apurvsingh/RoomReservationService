using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Infrastructure.Persistence;

namespace RoomReservation.Infrastructure.Repositories;

internal class ReservationRepository(RoomReservationsDbContext dbContext) : IReservationRepository
{
    public async Task<int> Create(Reservation entity)
    {
        dbContext.Reservations.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        var reservations = await dbContext.Reservations.ToListAsync();
        return reservations;
    }

    public async Task<Reservation?> GetReservationByTimeAsync(int id)
    {
        var reservations = await dbContext.Reservations
            .FirstOrDefaultAsync(r => r.Id == id);

        return reservations;
    }
}
