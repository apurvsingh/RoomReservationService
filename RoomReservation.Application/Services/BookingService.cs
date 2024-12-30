using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Booking;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IBookingService
{

    Task<IEnumerable<BookingDto>> GetBookings();
    //string ServiceName();
    //List<Booking> GetBookings(DateTime rangeStart, DateTime rangeEnd);
    //void CreateBooking(Booking booking);
}

internal class BookingService(
        IBookingRepository reservationRepository,
        IBookingMapper reservationMapper,
        ILogger<BookingService> logger) : IBookingService
{
    public async Task<IEnumerable<BookingDto>> GetBookings()
    {
        logger.LogInformation("Getting all clients");
        var reservations = await reservationRepository.GetAllReservationsAsync();

        return reservationMapper.MapToViewModelList(reservations);
    }
}
