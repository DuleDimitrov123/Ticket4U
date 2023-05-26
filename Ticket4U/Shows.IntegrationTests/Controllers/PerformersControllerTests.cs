using Shouldly;
using Shows.Api.Requests.Performers;
using Shows.Application.Features.Performers.Queries;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class PerformersControllerTests : BaseControllerTests
{
    public PerformersControllerTests(CustomWebApplicationFactory<Program> factory)
        :base(factory)
    {
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
}
