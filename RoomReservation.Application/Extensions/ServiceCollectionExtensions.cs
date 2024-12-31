using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Services;
using RoomReservation.Application.Utilities.Strategy.Booking;

namespace RoomReservation.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingMapper, BookingMapper>();
        services.AddScoped<IBookingStrategy, GoogleBookingStrategy>();
        services.AddScoped<IBookingStrategy, DefaultBookingStrategy>();

        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
    }
}
