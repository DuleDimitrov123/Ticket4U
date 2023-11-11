using Microsoft.Extensions.DependencyInjection;

namespace Shared.Api.Cors;

public static class CorsExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CORS", builder =>
            {
                builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            });
        });
    }
}
