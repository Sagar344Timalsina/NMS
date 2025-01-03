using Application;
using Domain;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure;
using Persistence;
using WebApi.Hubs;

namespace WebApi.Extentions;

public static class ServiceExtentions
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(
        options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(origin => true));
        });
    // public static void ConfigurePersistenceDI(this IServiceCollection services, IConfiguration configuration)=>AddPersistenceDI(configuration);
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<INotificationService,SignalRNotificationService>();
        services.AddApplicationDI(configuration);
        services.AddInfrastructureDI(configuration);
        services.AddPersistenceDI(configuration);
        return services;    
    }
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        var managers = typeof(IService);

        var types = managers
            .Assembly
            .GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name}"),
                Implementation = t
            })
            .Where(t => t.Service != null);

        foreach (var type in types)
        {
            if (managers.IsAssignableFrom(type.Service))
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }

        return services;
    }
}