using Microsoft.EntityFrameworkCore;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Infrastructure.Persistance;

public class ShowsDbContext : DbContext
{
    public DbSet<Performer> Performers { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Show> Shows { get; set; }

    public ShowsDbContext(DbContextOptions<ShowsDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowsDbContext).Assembly);
    }
}
