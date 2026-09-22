using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Api.Repositories;
using MySpot.Api.Services;

namespace MySpot.Infrastucture;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>();
        services.AddScoped<IClock, Clock>();
        return services;
    }
}