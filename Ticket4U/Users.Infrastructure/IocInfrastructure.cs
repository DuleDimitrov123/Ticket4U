using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Authentication;
using Shared.Infrastructure.Outbox;
using Users.Application.Contracts.Identity;
using Users.Application.Contracts.Outbox;
using Users.Application.Models.Identity;
using Users.Domain.Users;
using Users.Infrastructure.Identity;
using Users.Infrastructure.Identity.Services;
using Users.Infrastructure.Outbox;

namespace Users.Infrastructure;

public static class IocInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<UsersIdentityDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("UsersConnectionString"),
                b => b.MigrationsAssembly(typeof(UsersIdentityDbContext).Assembly.FullName));
        });

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<UsersIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthenticationService, AuthenticationService>();

        //Add CAP
        var capOptionsConstants = new CapOptionsConstants();
        configuration.Bind("CapOptionsConstants", capOptionsConstants);

        services.AddCap(options =>
        {
            options.FailedRetryCount = capOptionsConstants.FailedRetryCount;

            options.UseEntityFramework<UsersIdentityDbContext>();

            options.UseRabbitMQ(configuration.GetSection("EventBusSettings:Host").Value!);
        });
        services.AddScoped<IUserPublisher, UserPublisher>();

        services.AddCustomAuthentication(configuration);

        return services;
    }
}
