using AutoMapper;

namespace RoomReservation.Application.Dtos.Client;

public class ClientsProfile : Profile
{
    public ClientsProfile()
    {
        CreateMap<CreateClientDto, Domain.Entities.Client>();
        
        CreateMap<Domain.Entities.Client, ClientDto>()
            .ForMember(c => c.Reservations, opt => opt.MapFrom(src => src.Reservations));
    }
}
