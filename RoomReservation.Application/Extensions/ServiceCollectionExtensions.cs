using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Services;
using RoomReservation.Application.Utilities.Factory;
using RoomReservation.Application.Utilities.Strategy.Booking;

namespace RoomReservation.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingMapper, BookingMapper>();
        services.AddScoped<IBookingStrategy, GoogleBookingStrategy>();
        services.AddScoped<IBookingStrategy, Ms365BookingStrategy>();
        services.AddScoped<IBookingStrategy, DefaultBookingStrategy>();
        
        // Register factory
        services.AddScoped<IBookingStrategyFactory, BookingStrategyFactory>();

        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
    }
}
