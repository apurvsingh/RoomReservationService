using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Application.Clients;

namespace RoomReservation.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClientsService, ClientsService>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}
