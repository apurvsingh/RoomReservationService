using AutoMapper;
using RoomReservation.Application.Dtos.Booking;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<RoomReservation.Domain.Entities.Booking, BookingDto>();
    }
}
