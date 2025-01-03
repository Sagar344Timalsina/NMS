using Domain.Interfaces;
using Infrastructure.Messaging;
using Infrastructure.Services;
using Infrastructure.Services.Implementation;
using Infrastructure.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<INotificationManager, NotificationManager>();
        services.AddSingleton<RabitmqConnectionFactory>();
        services.AddScoped<IMessagePublisher, RabbitMqNotificationService>();

        services.AddStackExchangeRedisCache(options =>
        {
            string connection = configuration.GetConnectionString("Redis")!;
            options.Configuration = connection;

        });
        return services;
    }

}