using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Reservations.Infrastructure;

public static class IocInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: Add DbContext and Repos

        return services;
    }
}
