using AutoMapper;
using RoomReservation.Application.Commands.CreateClient;

namespace RoomReservation.Application.Dtos.Client;

public class ClientsProfile : Profile
{
    public ClientsProfile()
    {
        CreateMap<CreateClientCommand, Domain.Entities.Client>();
        
        CreateMap<Domain.Entities.Client, ClientDto>()
            .ForMember(c => c.Bookings, opt => opt.MapFrom(src => src.Bookings));
    }
}
