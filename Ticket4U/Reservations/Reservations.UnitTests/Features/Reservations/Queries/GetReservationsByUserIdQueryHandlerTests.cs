using AutoFixture.Xunit2;
using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Queries;

public class GetReservationsByUserIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Theory]
    [AutoMoqData]
    public async Task GetReservationByUserId(
        [Frozen] Mock<IReservationQueryRepository> reservationRepositoryMock,
        [Frozen] Mock<IQueryRepository<Show>> showRepositoryMock,
        [Frozen] Mock<IQueryRepository<User>> userRepositoryMock,
        GetReservationsByUserIdQueryHandler handler)
    {
        //arrange
        int numberOfReservationElements = 4;
        var reservations = new List<Reservation>();

        for (int i = 0; i < numberOfReservationElements; i++)
        {
            reservations.Add(Reservation.Create(Guid.NewGuid(), Guid.NewGuid(), NumberOfReservations.Create(3)));
        }

        var show = Show.Create(Guid.NewGuid(), "name", DateTime.Now.AddDays(1), 1000, Guid.NewGuid());
        var user = User.Create(Guid.NewGuid(), "email@gmail.com", "username", Guid.NewGuid());

        reservationRepositoryMock.Setup(r => r.GetReservationsByUserId(It.IsAny<Guid>())).ReturnsAsync(reservations);

        showRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        userRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(user);

        handler = new GetReservationsByUserIdQueryHandler(_mapper, reservationRepositoryMock.Object, showRepositoryMock.Object, userRepositoryMock.Object);

        //act
        var results = await handler.Handle(new GetReservationsByUserIdQuery() { UserId = Guid.NewGuid() }, CancellationToken.None);

        //assert
        results.Count.ShouldBe(numberOfReservationElements);

        for (int i = 0; i < numberOfReservationElements; i++)
        {
            results[i].Show.ShowId.ShouldBe(show.ExternalId);
            results[i].Show.ShowName.ShouldBe(show.Name);
            results[i].Show.ShowStartingDateTime.ShouldBe(show.StartingDateTime);
            results[i].UserId.ShouldBe(user.ExternalId);
            results[i].NumberOfReservations.ShouldBe(reservations[i].NumberOfReservations.Value);
        }
    }
}
