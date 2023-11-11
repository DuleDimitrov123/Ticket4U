using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Api.FluentValidation;

public static class FluentValidationExtensions
{
    public static void AddCustomFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidation(options =>
        {
            options.AutomaticValidationEnabled = true;
            options.ImplicitlyValidateChildProperties = true;
        });

        services.AddValidatorsFromAssemblies(new[] { assembly });
    }
}
