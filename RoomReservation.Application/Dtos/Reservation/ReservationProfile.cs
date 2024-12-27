using AutoMapper;
namespace RoomReservation.Application.Dtos.Reservation;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Domain.Entities.Reservation, ReservationDto>();
    }
}
