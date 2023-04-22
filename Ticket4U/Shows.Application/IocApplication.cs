using Microsoft.Extensions.DependencyInjection;

namespace Shows.Application;

public static class IocApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(IocApplication).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

        return services;
    }
}
