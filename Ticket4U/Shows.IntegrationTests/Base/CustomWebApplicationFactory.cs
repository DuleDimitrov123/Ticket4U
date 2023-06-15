using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Shows.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;
using Shows.Application.Contracts.Outbox;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shows.IntegrationTests.Overrides;

namespace Shows.IntegrationTests.Base;

public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class
{
    private readonly MsSqlTestcontainer _container =
        new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration
        {
            Database = "ShowsIntegrationTests",
            Password = "localdevpassword#123",
        })
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTestDbContext(_container.ConnectionString);

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