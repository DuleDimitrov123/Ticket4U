using Reservations.Application;
using Reservations.Infrastructure;
using Shared.Api.Cors;
using Shared.Api.FluentValidation;
using Shared.Api.Swagger;

namespace Reservations.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(IocApi).Assembly;

        services.AddApplication();

        services.AddInfrastructure(configuration);

        services.AddCustomFluentValidation(assembly);

        services.AddSwaggerSecurity("Reservations API", "v1");

        services.ConfigureCors();

        return services;
    }
}
