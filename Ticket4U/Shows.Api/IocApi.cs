using FluentValidation;
using FluentValidation.AspNetCore;
using Shows.Application;
using Shows.Infrastructure;

namespace Shows.Api;

public static class IocApi
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        #region FluentValidation

        services.AddFluentValidation(options =>
        {
            options.AutomaticValidationEnabled = true;
            options.ImplicitlyValidateChildProperties = true;
        });

        services.AddValidatorsFromAssemblies(new[] { typeof(IocApi).Assembly });

        #endregion

        return services;
    }
}
