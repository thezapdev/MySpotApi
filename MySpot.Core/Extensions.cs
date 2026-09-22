using Microsoft.Extensions.DependencyInjection;


namespace MySpot.Infrastucture;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}