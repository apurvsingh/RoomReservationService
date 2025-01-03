using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Common.RabbitMq;

namespace RoomReservation.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();
    }
}
