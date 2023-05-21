using Shouldly;
using Shows.Api.Requests.Categories;
using Shows.Api.Requests.Performers;
using Shows.Application.Features.Performers.Queries;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    [Fact]
    public async Task GetPerformerDetailByIdNotFound()
    {
        var response = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{Guid.NewGuid()}/{UrlConstants.SpecificPerformerDetail}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetPerformerDetailWithEmptyGuid()
    {
        var response = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{Guid.Empty}/{UrlConstants.SpecificPerformerDetail}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetPerformerDetailSuccessfully()
    {
        //first create performer
        var newPerformerInfo = new Dictionary<string, string>()
            {
                { "DateOfBirth", "01.01.1999." }
            };
        var newPerformer = new CreatePerformerRequest("Test Performer", newPerformerInfo);
        var performerId = await CreatePerformer(newPerformer);

        performerId.ShouldNotBe(Guid.Empty);

        //now get already created performer
        var performer = await GetPerformerDetail(performerId);

        performer!.Name.ShouldBe(newPerformer.Name);
        
        performer.PerformerInfos.Count.ShouldBe(1);
        performer.PerformerInfos.Select(pi => pi.Name).ShouldContain("DateOfBirth");
        performer.PerformerInfos.Select(pi => pi.Value).ShouldContain("01.01.1999.");
    }

    [Fact]
    public async Task CreatePerfromerSuccessfully()
    {
        var newPerformer = new CreatePerformerRequest("Test Performer", new Dictionary<string, string>());

        var performerId = await CreatePerformer(newPerformer);

        performerId.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdatePerformerInfoSuccessfully()
    {
        //first create performer with performer info
        var newPerformerInfo = new Dictionary<string, string>()
            {
                { "DateOfBirth", "01.01.1999." }
            };
        var newPerformer = new CreatePerformerRequest("Test Performer", newPerformerInfo);
        var performerId = await CreatePerformer(newPerformer);

        performerId.ShouldNotBe(Guid.Empty);

        //update performer info
        var updatePerformerInfoRequest = new UpdatePerformerInfoRequest(
            new Dictionary<string, string>()
            {
                { "DateOfBirth", "01.01.1998." },
                { "Number of concerts", "100" }
            });

        await UpdatePerformerInfo(performerId, updatePerformerInfoRequest);

        //get performer and verify added performer info
        var performer = await GetPerformerDetail(performerId);

        performer.PerformerInfos.Count.ShouldBe(2);
        performer.PerformerInfos.Select(pi => pi.Name).ShouldContain("DateOfBirth");
        performer.PerformerInfos.Select(pi => pi.Name).ShouldContain("Number of concerts");
        performer.PerformerInfos.Select(pi => pi.Value).ShouldContain("01.01.1998.");
        performer.PerformerInfos.Select(pi => pi.Value).ShouldContain("100");
    }

    [Fact]
    public async Task DeletePerformerInfoSuccessfully()
    {
        //first create performer with performer info
        var newPerformerInfo = new Dictionary<string, string>()
            {
                { "DateOfBirth", "01.01.1999." },
                { "Number of concerts", "100" }
            };
        var newPerformer = new CreatePerformerRequest("Test Performer", newPerformerInfo);
        var performerId = await CreatePerformer(newPerformer);

        performerId.ShouldNotBe(Guid.Empty);

        //delete performer info
        var deletePerformerInfoRequest = new DeletePerformerInfoRequest(new List<string>() { "DateOfBirth" });

        await DeletePerformerInfo(performerId, deletePerformerInfoRequest);

        //get performer and verify that performer info is deleted
        var performer = await GetPerformerDetail(performerId);

        performer.PerformerInfos.Count.ShouldBe(1);
        performer.PerformerInfos.Select(pi => pi.Name).ShouldNotContain("DateOfBirth");
        performer.PerformerInfos.Select(pi => pi.Name).ShouldContain("Number of concerts");
        performer.PerformerInfos.Select(pi => pi.Value).ShouldNotContain("01.01.1999.");
        performer.PerformerInfos.Select(pi => pi.Value).ShouldContain("100");
    }

    private async Task UpdatePerformerInfo(Guid performerInfo, UpdatePerformerInfoRequest updatePerformerInfoRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BasePerformerURL}/{performerInfo}/{UrlConstants.SpecificUpdatePerformerInfo}", 
            updatePerformerInfoRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    private async Task DeletePerformerInfo(Guid performerInfo, DeletePerformerInfoRequest deletePerformerInfoRequest)
    {
        var stringContent = new StringContent(JsonSerializer.Serialize(deletePerformerInfoRequest), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{UrlConstants.BasePerformerURL}/{performerInfo}/{UrlConstants.SpecificUpdatePerformerInfo}");
        request.Content = stringContent;

        var response = await _client.SendAsync(request);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
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

    private async Task<PerformerDetailResponse> GetPerformerDetail(Guid performerId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BasePerformerURL}/{performerId}/{UrlConstants.SpecificPerformerDetail}");
        var performer = await getResponse.Content.ReadFromJsonAsync<PerformerDetailResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        performer.ShouldNotBeNull();

        return performer;
    }
}
