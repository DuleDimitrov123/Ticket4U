using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Shows.Application.Contracts.Outbox;
using Shows.Infrastructure.Persistance;
using Shows.IntegrationTests.Overrides;
using Testcontainers.MsSql;

namespace Shows.IntegrationTests.Base;

public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    //If you want to connect to db, connectionString (change port): Server=localhost,52159;Database=master;User Id=sa;Password=localdevpassword#123;
    private readonly MsSqlContainer _container =
        new MsSqlBuilder()
            .WithPassword("localdevpassword#123")
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTestDbContext(_container.GetConnectionString());

            services.RemoveAll<IShowPublisher>();
            services.AddScoped<IShowPublisher, TestShowPublisher>();

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ShowsDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                context.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTestsAsync(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            };
        });
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();

    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }
}