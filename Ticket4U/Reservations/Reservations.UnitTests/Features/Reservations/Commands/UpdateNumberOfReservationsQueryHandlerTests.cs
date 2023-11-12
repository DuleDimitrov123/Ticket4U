using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class UpdateNumberOfReservationsQueryHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateNumberOfReservations([Frozen] Mock<ICommandRepository<Reservation>> reservationCommandRepositoryMock,
        [Frozen] Mock<IQueryRepository<Reservation>> reservationQueryRepositoryMock,
        [Frozen] Mock<IQueryRepository<Show>> showRepositoryMock,
        [Frozen] Mock<ICheckShowReservation> checkShowReservationMock,
        [Frozen] Mock<IMediator> mediatorMock,
        UpdateNumberOfReservationsCommandHandler handler)
    {
        //arrange
        var numberOfPlacesForShow = 10;
        var show = Show.Create("TestShow", DateTime.Now.AddDays(10), numberOfPlacesForShow, Guid.NewGuid());

        int numberOfReservations = 3;
        int newNumberOfReservations = 5;
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(numberOfReservations));
        var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
        reservationPropertyInfo!.SetValue(reservation, Guid.NewGuid());

        reservationQueryRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);

        showRepositoryMock.Setup(s => s.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        checkShowReservationMock
            .Setup(c => c.GetNumberOfAvailableReservations(It.IsAny<Show>()))
            .ReturnsAsync(show.NumberOfPlaces);

        handler = new UpdateNumberOfReservationsCommandHandler(
            reservationCommandRepositoryMock.Object,
            reservationQueryRepositoryMock.Object,
            showRepositoryMock.Object,
            checkShowReservationMock.Object,
            mediatorMock.Object);

        var command = new UpdateNumberOfReservationsCommand() { Id = reservation.Id, NewNumberOfReservations = newNumberOfReservations };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        reservationCommandRepositoryMock.Verify(x => x.Update(It.Is<Reservation>(x => x.Id == reservation.Id)), Times.Once);
    }
}
