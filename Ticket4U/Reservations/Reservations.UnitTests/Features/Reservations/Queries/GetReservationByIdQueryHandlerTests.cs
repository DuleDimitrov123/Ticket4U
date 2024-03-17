using AutoFixture.Xunit2;
using Moq;
using Reservations.Application.Features.Reservations.Queries.GetReservationById;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationByIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Theory]
    [AutoMoqData]
    public async Task GetReservationById(
        [Frozen] Mock<IQueryRepository<Reservation>> reservationRepositoryMock,
        [Frozen] Mock<IQueryRepository<Show>> showRepositoryMock,
        [Frozen] Mock<IQueryRepository<User>> userRepositoryMock,
        GetReservationByIdQueryHandler handler)
    {
        //arrange
        var reservation = Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3));
        var show = Show.Create(Guid.NewGuid(), "name", DateTime.Now.AddDays(1), 1000, Guid.NewGuid());
        var user = User.Create(Guid.NewGuid(), "email@gmail.com", "username", Guid.NewGuid());

        reservationRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(reservation);
        showRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);
        userRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(user);

        handler = new GetReservationByIdQueryHandler(_mapper, reservationRepositoryMock.Object, showRepositoryMock.Object, userRepositoryMock.Object);

        //act
        var result = await handler.Handle(new GetReservationByIdQuery() { ReservationId = Guid.NewGuid() }, CancellationToken.None);

        //assert
        result.ShouldNotBeNull();

        result.Show.ShowId.ShouldBe(show.ExternalId);
        result.Show.ShowName.ShouldBe(show.Name);
        result.Show.ShowStartingDateTime.ShouldBe(show.StartingDateTime);
        result.UserId.ShouldBe(user.ExternalId);
        result.NumberOfReservations.ShouldBe(reservation.NumberOfReservations.Value);
    }
}
