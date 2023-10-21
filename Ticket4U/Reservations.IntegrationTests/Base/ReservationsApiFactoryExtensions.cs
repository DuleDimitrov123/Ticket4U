﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Infrastructure;

namespace Reservations.IntegrationTests.Base;

public static class ReservationsApiFactoryExtensions
{
    public static void AddTestDbContext(this IServiceCollection services, string connectionString)
    {
        var dbContextOptionsDescriptor = services.SingleOrDefault
            (d => d.ServiceType == typeof(DbContextOptions<ReservationsDbContext>));

        if (dbContextOptionsDescriptor != null)
        {
            services.Remove(dbContextOptionsDescriptor);
        }

        services.AddDbContext<ReservationsDbContext>(options =>
        {
            var connString = connectionString + ";TrustServerCertificate=True"; // EF 7
            options.UseSqlServer(connString);
        });
    }
}
