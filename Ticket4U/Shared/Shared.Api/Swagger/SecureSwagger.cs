using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shared.Api.Swagger;

public static class SecureSwagger
{
    public static void AddSwaggerSecurity(this IServiceCollection services, string serviceName, string version)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc(version, new OpenApiInfo { Title = serviceName, Version = version });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}
