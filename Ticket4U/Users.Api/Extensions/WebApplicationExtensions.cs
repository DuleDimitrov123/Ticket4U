using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Application.Contracts.Identity;
using Users.Application.Models.Identity;
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
        var user1 = new RegistrationRequest
        {
            FirstName = "Dusan",
            LastName = "Dimitrov",
            Email = "dusan.dimitrov@gmail.com",
            Password = "TestPass12*4NotReal",
            UserName = "DuleDimitrov"
        };

        try
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var authenticationService = serviceScope.ServiceProvider.GetRequiredService<IAuthenticationService>();

                await authenticationService.RegistrateAsync(user1, true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}
