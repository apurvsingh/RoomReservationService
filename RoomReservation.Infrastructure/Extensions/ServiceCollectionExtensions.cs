using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Domain.Repositories;
using RoomReservation.Infrastructure.Persistence;
using RoomReservation.Infrastructure.Repositories;
using RoomReservation.Infrastructure.Seeders;

namespace RoomReservation.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RoomReservationsDb");
        services.AddDbContext<RoomReservationsDbContext>(options =>options.UseSqlServer(connectionString));
    
        services.AddScoped<IClientSeeder, ClientSeeder>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
    }
}
