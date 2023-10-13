using Shared.IntegrationTests.Authorization;
using Shouldly;
using Shows.Api.Requests.Shows;
using Shows.Application.Features.Shows.Queries;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class ShowsControllerTests : BaseControllerTests
{
    public ShowsControllerTests(CustomWebApplicationFactory<Program> factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task GetShowsSuccessResult()
    {
        _client.SetAuthorization(AuthorizationType.BasicAuthorization);

        var response = await _client.GetAsync(UrlConstants.BaseShowURL);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<ShowResponse>>(responseString, _jsonSerializerOptions);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<ShowResponse>>();
        result.Count.ShouldNotBe(0);
    }

    [Fact]
    public async Task GetShowByIdNotFound()
    {
        _client.SetAuthorization(AuthorizationType.BasicAuthorization);

        var response = await _client.GetAsync($"{UrlConstants.BaseShowURL}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetShowWithEmptyGuid()
    {
        _client.SetAuthorization(AuthorizationType.BasicAuthorization);

        var response = await _client.GetAsync($"{UrlConstants.BaseShowURL}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetShowSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //first create show
        var (showId, newShow) = await CreateDefaultShow();

        showId.ShouldNotBe(Guid.Empty);

        //now get already created show
        var show = await GetShow(showId);

        show.Name.ShouldBe(newShow.Name);
        show.Location.ShouldBe(newShow.Location);
        show.NumberOfplaces.ShouldBe(newShow.NumberOfplaces);
        show.TicketPriceCurrency.ShouldBe(newShow.TicketPriceCurrency);
        show.TickerPriceAmount.ShouldBe(newShow.TickerPriceAmount);
        show.StartingDateTime.ShouldBe(newShow.StartingDateTime);
        show.PerformerId.ShouldBe(newShow.PerformerId);
        show.CategoryId.ShouldBe(newShow.CategoryId);
    }

    [Fact]
    public async Task CreateShowSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        var (showId, _) = await CreateDefaultShow();

        showId.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdateShowNameSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //first create show
        var (showId, _) = await CreateDefaultShow();

        //update show name
        var newShowName = "Test show 2 new name";
        var updateShowName = new UpdateShowNameRequest(newShowName);

        await UpdateShowName(showId, updateShowName);

        //get show and verify updated show name
        var showWithUpdatedName = await GetShow(showId);

        showWithUpdatedName.Name.ShouldBe(newShowName);
    }

    [Fact]
    public async Task UpdateShowLocationSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //first create show
        var (showId, _) = await CreateDefaultShow();

        //update show location
        var newShowLocation = "Test location new ";
        var updateShowLocation = new UpdateShowLocationRequest(newShowLocation);

        await UpdateShowLocation(showId, updateShowLocation);

        //get show and verify updated show location
        var showWithUpdatedLocation = await GetShow(showId);

        showWithUpdatedLocation.Location.ShouldBe(newShowLocation);
    }

    [Fact]
    public async Task UpdateShowPriceSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //first create show
        var (showId, _) = await CreateDefaultShow();

        //update show price
        var newShowPrice = 150;
        var updateShowPriceRequest = new UpdateShowPriceRequest(newShowPrice);

        await UpdateShowPrice(showId, updateShowPriceRequest);

        //get show and verify updated show price
        var showWithUpdatedPrice = await GetShow(showId);

        showWithUpdatedPrice.TickerPriceAmount.ShouldBe(newShowPrice);
    }

    [Fact]
    public async Task UpdateShowStartingDateTimeSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //first create show
        var (showId, _) = await CreateDefaultShow();

        //update show starting date time
        var newShowPrStartingDateTime = DateTime.Now.AddDays(50);
        var updateShowStartingDateTimeRequest = new UpdateShowStartingDateTimeRequest(newShowPrStartingDateTime);

        await UpdateShowStartingDateTime(showId, updateShowStartingDateTimeRequest);

        //get show and verify updated show starting date time
        var showWithUpdatedStartingDateTime = await GetShow(showId);

        showWithUpdatedStartingDateTime.StartingDateTime.ShouldBe(newShowPrStartingDateTime);
    }

    [Fact]
    public async Task DeleteShowSuccesffully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //create a show
        var (showId, _) = await CreateDefaultShow();

        //get show
        _ = await GetShow(showId);

        //delete
        await DeleteShow(showId);

        //get-doesn't exist

        var (_, statusCode) = await GetShowWithoutCheckingResponse(showId);
        statusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AddShowMessageSuccessfully()
    {
        _client.SetAuthorization(AuthorizationType.AdminAuthorization);

        //create show
        var (showId, _) = await CreateDefaultShow();

        //add show message
        var addShowMessageRequest = new AddShowMessageRequest("NewShowMessageName", "NewShowMessageValue");
        await AddShowMessage(showId, addShowMessageRequest);

        //get detail and check that show message exists
        var showDetail = await GetShowDetail(showId);

        showDetail.ShowMessages.Select(sm => sm.Name)
            .ShouldContain(addShowMessageRequest.ShowMessageName);

        showDetail.ShowMessages.Select(sm => sm.Value)
            .ShouldContain(addShowMessageRequest.ShowMessageValue);
    }
}
