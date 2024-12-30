using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Services;

namespace RoomReservation.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingMapper, BookingMapper>();

        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
    }
}
