using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Commands.CreateReservation;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Reservations.UnitTests.Helpers;
using Shared.Application.Contracts.Persistence;
using Shouldly;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class CreateReservationCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task CreateNewReservation([Frozen] Mock<ICommandRepository<Reservation>> mockReservationCommandRepository,
        [Frozen] Mock<IQueryRepository<Show>> mockShowRepository,
        [Frozen] Mock<IUserQueryRepository> mockUserRepository,
        [Frozen] Mock<ICheckShowReservation> mockCheckShowReservation,
        [Frozen] Mock<IMediator> mockMediator,
        CreateReservationCommandHandler handler)
    {
        //arrange
        var reservations = new List<Reservation>();
        var show = Show.Create("Show1", DateTime.Now.AddDays(10), 100, Guid.NewGuid());
        var user = User.Create("user1@gmail.com", "user1", Guid.NewGuid());

        mockReservationCommandRepository.Setup(repo => repo.Add(It.IsAny<Reservation>()))
            .ReturnsAsync(
                (Reservation res) =>
                {
                    //set Id by reflection...repo is mocked and won't create one
                    var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
                    reservationPropertyInfo!.SetValue(res, Guid.NewGuid());

                    reservations.Add(res);
                    return res;
                });

        mockShowRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        mockUserRepository.Setup(repo => repo.GetUserByExternalId(It.IsAny<Guid>())).ReturnsAsync(user);

        var command = new CreateReservationCommand()
        {
            ShowId = Guid.NewGuid(),
            ExternalUserId = Guid.NewGuid(),
            NumberOfReservations = 3
        };

        mockCheckShowReservation
            .Setup(c => c.GetNumberOfAvailableReservations(It.IsAny<Show>()))
            .ReturnsAsync(show.NumberOfPlaces);

        handler = new CreateReservationCommandHandler(
            mockReservationCommandRepository.Object,
            mockShowRepository.Object,
            mockUserRepository.Object,
            mockCheckShowReservation.Object,
            mockMediator.Object);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.ShouldNotBe(Guid.Empty);
    }
}
