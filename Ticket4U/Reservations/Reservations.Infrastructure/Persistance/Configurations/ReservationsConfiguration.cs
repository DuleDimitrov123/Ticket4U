using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;

namespace Reservations.Infrastructure.Persistance.Configurations;

public class ReservationsConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(reservation => reservation.Id);

        builder.Property(reservation => reservation.NumberOfReservations)
            .HasConversion(
                numberOfReservations => numberOfReservations.Value,
                value => NumberOfReservations.Create(value));

        builder.HasOne<Show>()
            .WithMany()
            .HasForeignKey(reservation => reservation.ShowId);

        builder.HasOne<Show>()
            .WithMany()
            .HasForeignKey(reservation => reservation.ShowId);
    }
}
