using Reservations.Application;
using Reservations.Infrastructure;

namespace Reservations.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();

        services.AddInfrastructure(configuration);

        return services;
    }
}
