using FluentValidation;
using FluentValidation.AspNetCore;
using Reservations.Application;
using Reservations.Infrastructure;

namespace Reservations.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(IocApi).Assembly;

        services.AddApplication();

        services.AddInfrastructure(configuration);

        services.AddFluentValidation(options =>
        {
            options.AutomaticValidationEnabled = true;
            options.ImplicitlyValidateChildProperties = true;
        });

        services.AddValidatorsFromAssemblies(new[] { assembly });

        return services;
    }
}
