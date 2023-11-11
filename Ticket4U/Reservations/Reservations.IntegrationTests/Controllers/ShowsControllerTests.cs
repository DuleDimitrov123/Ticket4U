using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Reservations.IntegrationTests.Helpers;
using Shared.Domain.Events;
using Shouldly;
using System.Net;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Controllers;

public class ShowsControllerTests : ShowsControllerHelper
{
    public ShowsControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
    }

    [Fact]
    public async Task CreateShowSuccessfully()
    {
        var createdShowEvent = new CreatedShowEvent()
        {
            Name = "TestShow",
            StartingDateTime = DateTime.Now.AddDays(10),
            NumberOfPlaces = 300,
            ExternalId = Guid.NewGuid()
        };

        var (statusCode, result) = await CreateShow<Guid>(createdShowEvent, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdateShowStartingDateTimeSuccessfully()
    {
        //update
        var newStartingDateTime = DateTime.Now.AddDays(11);
        var updatedShowsStartingDateTimeEvent = new UpdatedShowsStartingDateTimeEvent(InstanceConstants.ExternalShow1Id, newStartingDateTime, new DateTime());

        var statusCode = await UpdateShowStartingDateTime(updatedShowsStartingDateTimeEvent, false);

        statusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateShowStartingDateTimeNotFound()
    {
        //update
        var newStartingDateTime = DateTime.Now.AddDays(11);
        var updatedShowsStartingDateTimeEvent = new UpdatedShowsStartingDateTimeEvent(Guid.NewGuid(), newStartingDateTime, new DateTime());

        var statusCode = await UpdateShowStartingDateTime(updatedShowsStartingDateTimeEvent, false);

        statusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
