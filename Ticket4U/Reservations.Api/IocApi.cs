using Reservations.Infrastructure;

namespace Reservations.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: All all required dependencies and IoCInfrastructure and IoCApplication
        services.AddInfrastructure(configuration);

        return services;
    }
}
