using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;
using Reservations.Domain.Reservations;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class UpdateNumberOfReservationsQueryHandlerTests
{
    [Fact]
    public async Task UpdateNumberOfReservations()
    {
        //arrange
        int numberOfReservations = 3;
        int newNumberOfReservations = 5;
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(numberOfReservations));
        var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
        reservationPropertyInfo!.SetValue(reservation, Guid.NewGuid());

        var repositoryMock = new Mock<IRepository<Reservation>>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);

        var handler = new UpdateNumberOfReservationsQueryHandler(repositoryMock.Object);

        var command = new UpdateNumberOfReservationsQuery() { Id = reservation.Id, NewNumberOfReservations = newNumberOfReservations };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        repositoryMock.Verify(x => x.Update(It.Is<Reservation>(x => x.Id == reservation.Id)), Times.Once);
    }
}
