using Users.Application;
using Users.Infrastructure;

namespace Users.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        return services;
    }
}
