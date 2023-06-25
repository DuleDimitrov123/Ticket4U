using MediatR;
using Moq;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Commands.CreateReservation;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shouldly;
using System.Reflection;

namespace Reservations.UnitTests.Features.Reservations.Commands;

public class CreateReservationCommandHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task CreateNewReservation()
    {
        //arrange
        var reservations = new List<Reservation>();
        var show = Show.Create("Show1", DateTime.Now.AddDays(10), 100, Guid.NewGuid());
        var user = User.Create("user1@gmail.com", Guid.NewGuid());

        var mockReservationRepository = new Mock<IRepository<Reservation>>();
        mockReservationRepository.Setup(repo => repo.Add(It.IsAny<Reservation>()))
            .ReturnsAsync(
                (Reservation res) =>
                {
                    //set Id by reflection...repo is mocked and won't create one
                    var reservationPropertyInfo = typeof(Reservation).GetProperty("Id");
                    reservationPropertyInfo!.SetValue(res, Guid.NewGuid());

                    reservations.Add(res);
                    return res;
                });

        var mockShowRepository = new Mock<IRepository<Show>>();
        mockShowRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        var mockUserRepository = new Mock<IRepository<User>>();
        mockUserRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(user);

        var command = new CreateReservationCommand()
        {
            ShowId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            NumberOfReservations = 3
        };

        var mockCheckShowReservation = new Mock<ICheckShowReservation>();
        mockCheckShowReservation
            .Setup(c => c.GetNumberOfAvailableReservations(It.IsAny<Show>()))
            .ReturnsAsync(show.NumberOfPlaces);

        var handler = new CreateReservationCommandHandler(
            mockReservationRepository.Object, 
            mockShowRepository.Object, 
            mockUserRepository.Object,
            mockCheckShowReservation.Object,
            new Mock<IMediator>().Object);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.ShouldNotBe(Guid.Empty);
    }
}
