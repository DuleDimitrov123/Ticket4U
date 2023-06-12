using Microsoft.Extensions.DependencyInjection;

namespace Reservations.Application;

public static class IocApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(IocApplication).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

        return services;
    }
}
