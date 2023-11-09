using Shared.Api.Cors;
using Shared.Api.FluentValidation;
using Shared.Api.Swagger;
using Shows.Application;
using Shows.Infrastructure;

namespace Shows.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(IocApi).Assembly;

        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddAutoMapper(assembly);

        services.AddCustomFluentValidation(assembly);

        services.AddSwaggerSecurity("Shows API", "v1");

        services.ConfigureCors();

        return services;
    }
}
