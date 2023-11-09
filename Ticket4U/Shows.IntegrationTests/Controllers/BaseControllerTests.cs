using Microsoft.AspNetCore.Http;
using Shouldly;
using Shows.Api.Requests.Categories;
using Shows.Api.Requests.Performers;
using Shows.Api.Requests.Shows;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Performers.Queries;
using Shows.Application.Features.Shows.Queries;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly CustomWebApplicationFactory<Program> _factory;
    protected readonly HttpClient _client;
    protected readonly JsonSerializerOptions _jsonSerializerOptions;

    public BaseControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.GetAnonymousClient();

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    #region Categories

    protected async Task<CategoryResponse> GetCategory(Guid categoryId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}");
        var category = await getResponse.Content.ReadFromJsonAsync<CategoryResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        category.ShouldNotBeNull();

        return category;
    }

    protected async Task UpdateCategory(Guid categoryId, UpdateCategoryRequest updateCategoryRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}", updateCategoryRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task<Guid> CreateCategory(CreateCategoryRequest createCategoryRequest)
    {
        var createResponse = await _client.PostAsJsonAsync($"{UrlConstants.BaseCategoryURL}", createCategoryRequest);
        var categoryId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        return categoryId;
    }

    protected async Task ArchiveCategory(Guid categoryId)
    {
        var response = await _client.PutAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}/{UrlConstants.SpecificArchiveCategoryURL}", null);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    #endregion

    #region Performers

    protected async Task UpdatePerformerInfo(Guid performerInfo, UpdatePerformerInfoRequest updatePerformerInfoRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BasePerformerURL}/{performerInfo}/{UrlConstants.SpecificUpdatePerformerInfo}",
            updatePerformerInfoRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task DeletePerformerInfo(Guid performerInfo, DeletePerformerInfoRequest deletePerformerInfoRequest)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(deletePerformerInfoRequest), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{UrlConstants.BasePerformerURL}/{performerInfo}/{UrlConstants.SpecificUpdatePerformerInfo}");
        request.Content = stringContent;

        var response = await _client.SendAsync(request);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task<Guid> CreatePerformer(CreatePerformerRequest createPerformerRequest)
    {
        var createResponse = await _client.PostAsJsonAsync($"{UrlConstants.BasePerformerURL}", createPerformerRequest);
        var performerId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        return performerId;
    }

    protected async Task<PerformerResponse> GetPerformer(Guid performerId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{performerId}");
        var performer = await getResponse.Content.ReadFromJsonAsync<PerformerResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        performer.ShouldNotBeNull();

        return performer;
    }

    protected async Task<PerformerDetailResponse> GetPerformerDetail(Guid performerId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{performerId}/{UrlConstants.SpecificPerformerDetail}");
        var performer = await getResponse.Content.ReadFromJsonAsync<PerformerDetailResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        performer.ShouldNotBeNull();

        return performer;
    }

    #endregion

    #region Shows

    protected async Task<ShowResponse> GetShow(Guid showId)
    {
        var (show, statusCode) = await GetShowWithoutCheckingResponse(showId);

        statusCode.ShouldBe(HttpStatusCode.OK);

        show.ShouldNotBeNull();

        return show;
    }

    protected async Task<(ShowResponse, HttpStatusCode)> GetShowWithoutCheckingResponse(Guid showId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BaseShowURL}/{showId}");
        var show = await getResponse.Content.ReadFromJsonAsync<ShowResponse>();

        return (show, getResponse.StatusCode);
    }

    protected async Task<ShowDetailResponse> GetShowDetail(Guid showId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificShowDetail}");
        var showWithDetails = await getResponse.Content.ReadFromJsonAsync<ShowDetailResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        showWithDetails.ShouldNotBeNull();

        return showWithDetails;
    }

    protected async Task<(Guid, CreateShowRequest)> CreateDefaultShow()
    {
        //create perfromer
        var performerId = await CreatePerformer(new CreatePerformerRequest("Test Performer", new Dictionary<string, string>()));

        //create category
        var categoryId = await CreateCategory(new CreateCategoryRequest("Test Category", "Description of Test Category"));

        var newShow = new CreateShowRequest()
        {
            Name = "Test show 2",
            Description = "ShowDescription",
            Location = "Test location",
            NumberOfplaces = 100,
            TicketPriceCurrency = "RSD",
            TickerPriceAmount = 100,
            StartingDateTime = DateTime.Now.AddDays(30),
            PerformerId = performerId,
            CategoryId = categoryId
        };

        var r = await CreateShow(newShow);

        return (r, newShow);
    }

    protected async Task<Guid> CreateShow(CreateShowRequest createShowRequest)
    {
        var createResponse = await _client.PostAsJsonAsync($"{UrlConstants.BaseShowURL}", createShowRequest);
        var showId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        return showId;
    }

    protected async Task UpdateShowName(Guid showId, UpdateShowNameRequest updateShowNameRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificUpdateShowName}", updateShowNameRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task UpdateShowLocation(Guid showId, UpdateShowLocationRequest updateShowLocationRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificUpdateShowLocation}", updateShowLocationRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task UpdateShowPrice(Guid showId, UpdateShowPriceRequest updateShowPriceRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificUpdateShowPrice}", updateShowPriceRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task UpdateShowStartingDateTime(Guid showId, UpdateShowStartingDateTimeRequest updateShowStartingDateTimeRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificUpdateShowStartingDateTime}", updateShowStartingDateTimeRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task DeleteShow(Guid showId)
    {
        var response = await _client.DeleteAsync($"{UrlConstants.BaseShowURL}/{showId}");

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    protected async Task AddShowMessage(Guid showId, AddShowMessageRequest addShowMessageRequest)
    {
        var response = await _client.PostAsJsonAsync($"{UrlConstants.BaseShowURL}/{showId}/{UrlConstants.SpecificAddShowMessage}", addShowMessageRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    #endregion
}
