using MediatR;
using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class UpdateNumberOfReservationsQueryHandlerTests
{
    [Fact]
    public async Task UpdateNumberOfReservations()
    {
        //arrange
        var numberOfPlacesForShow = 10;
        var show = Show.Create("TestShow", DateTime.Now.AddDays(10), numberOfPlacesForShow, Guid.NewGuid());

        int numberOfReservations = 3;
        int newNumberOfReservations = 5;
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(numberOfReservations));
        var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
        reservationPropertyInfo!.SetValue(reservation, Guid.NewGuid());

        var reservationRepositoryMock = new Mock<IRepository<Reservation>>();
        reservationRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);

        var showRepositoryMock = new Mock<IRepository<Show>>();
        showRepositoryMock.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        var checkShowReservationMock = new Mock<ICheckShowReservation>();
        checkShowReservationMock
            .Setup(c => c.GetNumberOfAvailableReservations(It.IsAny<Show>()))
            .ReturnsAsync(show.NumberOfPlaces);

        var handler = new UpdateNumberOfReservationsQueryHandler(
            reservationRepositoryMock.Object,
            showRepositoryMock.Object,
            checkShowReservationMock.Object,
            new Mock<IMediator>().Object);

        var command = new UpdateNumberOfReservationsQuery() { Id = reservation.Id, NewNumberOfReservations = newNumberOfReservations };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        reservationRepositoryMock.Verify(x => x.Update(It.Is<Reservation>(x => x.Id == reservation.Id)), Times.Once);
    }
}
