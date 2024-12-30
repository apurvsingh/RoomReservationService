using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<int> Create(Reservation entity);
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByTimeAsync(int id);
    }
}
