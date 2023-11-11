using Microsoft.EntityFrameworkCore;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;

namespace Reservations.Infrastructure;

public class ReservationsDbContext : DbContext
{
    public DbSet<Show> Shows { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationsDbContext).Assembly);
    }
}
