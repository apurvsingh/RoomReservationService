using Microsoft.Extensions.Logging;
using RoomReservation.Application.Dtos.Reservation;
using RoomReservation.Application.Mappers.Reservation;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;

namespace RoomReservation.Application.Services;


public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllReservations();
}

internal class ReservationService(
        IReservationRepository reservationRepository,
        IReservationMapper reservationMapper,
        ILogger<ReservationService> logger) : IReservationService
{
    public async Task<IEnumerable<ReservationDto>> GetAllReservations()
    {
        logger.LogInformation("Getting all clients");
        var reservations = await reservationRepository.GetAllReservationsAsync();

        return reservationMapper.MapToViewModelList(reservations);
    }
}
