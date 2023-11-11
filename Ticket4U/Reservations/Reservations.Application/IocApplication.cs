using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.Services;

namespace Reservations.Application;

public static class IocApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(IocApplication).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assembly));

        services.AddScoped<ICheckShowReservation, CheckShowReservation>();

        return services;
    }
}
