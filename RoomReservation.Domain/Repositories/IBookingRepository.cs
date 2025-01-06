using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task<int> Create(Booking entity);
        Task CreateForRabbitMq(Booking entity);
        Task<IEnumerable<Booking>> GetAllReservationsAsync();
        Task<List<Booking>> GetAllBookingsByClientIdAsync(Booking booking);
    }
}
