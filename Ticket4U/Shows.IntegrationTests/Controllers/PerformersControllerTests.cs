using Shouldly;
using Shows.Api.Requests.Categories;
using Shows.Api.Requests.Performers;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Performers.Queries;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class PerformersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public PerformersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.GetAnonymousClient();

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetPerformersSuccessResult()
    {
        var response = await _client.GetAsync(UrlConstants.BasePerformerURL);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<PerformerResponse>>(responseString, _jsonSerializerOptions);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<PerformerResponse>>();
        result.Count.ShouldNotBe(0);
    }

    [Fact]
    public async Task GetPerformerByIdNotFound()
    {
        var response = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetPerformerWithEmptyGuid()
    {
        var response = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetPerformerSuccessfully()
    {
        //first create performer
        var newPerformer = new CreatePerformerRequest("Test Performer", new Dictionary<string, string>());
        var performerId = await CreatePerformer(newPerformer);

        performerId.ShouldNotBe(Guid.Empty);

        //now get already created performer
        var performer = await GetPerformer(performerId);

        performer!.Name.ShouldBe(newPerformer.Name);
    }

    private async Task<Guid> CreatePerformer(CreatePerformerRequest createPerformerRequest)
    {
        var createResponse = await _client.PostAsJsonAsync($"{UrlConstants.BasePerformerURL}", createPerformerRequest);
        var performerId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        return performerId;
    }

    private async Task<PerformerResponse> GetPerformer(Guid performerId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{performerId}");
        var performer = await getResponse.Content.ReadFromJsonAsync<PerformerResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        performer.ShouldNotBeNull();

        return performer;
    }
}
