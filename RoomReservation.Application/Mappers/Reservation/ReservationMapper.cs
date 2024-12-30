using RoomReservation.Application.Dtos.Reservation;

namespace RoomReservation.Application.Mappers.Reservation;

public interface IReservationMapper
{
    ReservationDto MapToViewModel(Domain.Entities.Reservation reservation);
    IEnumerable<ReservationDto> MapToViewModelList(IEnumerable<Domain.Entities.Reservation> reservations);
}

internal class ReservationMapper : IReservationMapper
{
    public ReservationDto MapToViewModel(Domain.Entities.Reservation reservation)
    {
        return new ReservationDto()
        {
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            RoomId = reservation.RoomId,
            Title = reservation.Title,
        };
    }

    public IEnumerable<ReservationDto> MapToViewModelList(IEnumerable<Domain.Entities.Reservation> reservations)
    {
        List<ReservationDto> result = new List<ReservationDto>();
        
        foreach (var item in reservations)
        {
            result.Add(MapToViewModel(item));
        }
        return result;
    }
}
