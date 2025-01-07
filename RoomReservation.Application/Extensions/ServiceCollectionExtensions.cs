using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Application.Mappers.Booking;
using RoomReservation.Application.Services;
using RoomReservation.Application.Services.Redis;
using RoomReservation.Application.Utilities.Factory;
using RoomReservation.Application.Utilities.Strategy.Booking;
using StackExchange.Redis;

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

        // Configure Redis
        var redisConnectionString = "localhost:6379";
        var multiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddScoped<IRedisCacheService, RedisCacheService>();

        // Register factory
        services.AddScoped<IBookingStrategyFactory, BookingStrategyFactory>();

        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
    }
}
