using FluentValidation;
using FluentValidation.AspNetCore;
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

        #region FluentValidation

        services.AddFluentValidation(options =>
        {
            options.AutomaticValidationEnabled = true;
            options.ImplicitlyValidateChildProperties = true;
        });

        services.AddValidatorsFromAssemblies(new[] { assembly });

        #endregion

        services.AddSwaggerSecurity("Shows API", "v1");

        return services;
    }
}
