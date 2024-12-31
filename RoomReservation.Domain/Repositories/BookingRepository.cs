using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task<int> Create(Booking entity);
        Task<IEnumerable<Booking>> GetAllReservationsAsync();
        Task<Booking?> GetReservationByTimeAsync(int id, Booking bookingRequestDto);
    }
}
