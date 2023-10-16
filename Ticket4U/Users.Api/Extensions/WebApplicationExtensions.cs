using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Application.Features.Users.Commands.RegistrateUser;
using Users.Infrastructure.Identity;

namespace Users.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void AddDBMigrations(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<UsersIdentityDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not run migrations: {ex.Message}");
            }
        }
    }

    public async static Task AddUserRoles(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }

    public async static Task AddDefaultAdminUsers(this WebApplication app)
    {
        var user1 = new RegistrateUserCommand
        {
            FirstName = "Dusan",
            LastName = "Dimitrov",
            Email = "dusan.dimitrov@gmail.com",
            Password = "TestPass12*4NotReal",
            UserName = "DuleDimitrov"
        };

        using (var serviceScope = app.Services.CreateScope())
        {
            var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(user1);
        }
    }
}
