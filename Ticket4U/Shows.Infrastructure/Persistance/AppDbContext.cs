using Microsoft.EntityFrameworkCore;
using Shows.Domain.Categories;
using Shows.Domain.Performers;

namespace Shows.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<Performer> Performers { get; set; }

    public DbSet<Category> Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
