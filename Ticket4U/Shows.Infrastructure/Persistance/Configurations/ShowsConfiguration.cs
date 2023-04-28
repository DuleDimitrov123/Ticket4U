using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Infrastructure.Persistance.Configurations;

public class ShowsConfiguration : IEntityTypeConfiguration<Show>
{
    public void Configure(EntityTypeBuilder<Show> builder)
    {
        builder.HasKey(show => show.Id);

        builder.Property(show => show.Status)
            .HasConversion(
                status => status.ToString(),
                value => new ShowStatus().Create(value));

        builder.Property(show => show.NumberOfPlaces)
            .HasConversion(
                numberOfPlaces => numberOfPlaces.Value,
                value => NumberOfPlaces.Create(value));

        builder.OwnsOne(show => show.TicketPrice, tickerPriceBuilder =>
        {
            tickerPriceBuilder.Property(money => money.Currency).HasMaxLength(3);
        });

        builder.HasMany(show => show.ShowMessages)
            .WithOne()
            .HasForeignKey(showMessage => showMessage.ShowId);

        builder.HasOne<Performer>()
            .WithMany()
            .HasForeignKey(show => show.PerformerId)
            .IsRequired();

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(show => show.CategoryId)
            .IsRequired();
    }
}
