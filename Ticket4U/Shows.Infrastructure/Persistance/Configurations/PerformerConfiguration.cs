using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shows.Domain.Performers;

namespace Shows.Infrastructure.Persistance.Configurations;

public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
{
    public void Configure(EntityTypeBuilder<Performer> builder)
    {
        builder.HasKey(performer => performer.Id);

        builder.HasMany(performer => performer.PerformerInfos)
            .WithOne()
            .HasForeignKey(performerInfo => performerInfo.PerformerId);
    }
}
