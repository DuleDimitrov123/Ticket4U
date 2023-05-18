using Azure;
using EmptyFiles;
using Shouldly;
using Shows.Api.Requests.Categories;
using Shows.Application.Features.Categories.Queries;
using Shows.Domain.Categories;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class CategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public CategoriesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.GetAnonymousClient();

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetCategoriesSuccessResult()
    {
        var response = await _client.GetAsync(UrlConstants.BaseCategoryURL);

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<CategoryResponse>>(responseString, _jsonSerializerOptions);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<List<CategoryResponse>>();
        result.Count.ShouldNotBe(0);
    }

    [Fact]
    public async Task GetPersonByIdNotFound()
    {
        var response = await _client.GetAsync($"{UrlConstants.BaseCategoryURL}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetPersonWithEmptyGuid()
    {
        var response = await _client.GetAsync($"{UrlConstants.BaseCategoryURL}/{Guid.Empty}");

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetCategorySuccessfully()
    {
        //first create category
        var newCategory = new CreateCategoryRequest("Test Category", "Description of Test Category");
        var categoryId = await CreateCategory(newCategory);

        categoryId.ShouldNotBe(Guid.Empty);

        //now get already created category
        var category = await GetCategory(categoryId);

        category!.Name.ShouldBe(newCategory.Name);
        category!.Description.ShouldBe(newCategory.Description);
        category!.Status.ShouldBe(CategoryStatus.IsValid.ToString());
    }

    [Fact]
    public async Task CreateCategorySuccessfully()
    {
        var newCategory = new CreateCategoryRequest("Test Category", "Description of Test Category");

        var categoryId = await CreateCategory(newCategory);

        categoryId.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdateCategorySuccessfully()
    {
        //first create category
        var newCategory = new CreateCategoryRequest("Test Category", "Description of Test Category");
        var categoryId = await CreateCategory(newCategory);

        categoryId.ShouldNotBe(Guid.Empty);

        //update category
        var updatedCategory = new UpdateCategoryRequest("Test Category Updated", "Description of Test Category Updated");

        await UpdateCategory(categoryId, updatedCategory);

        //get category and verify updated fields
        var category = await GetCategory(categoryId);

        category!.Name.ShouldBe(updatedCategory.NewName);
        category!.Description.ShouldBe(updatedCategory.NewDescription);
    }

    private async Task UpdateCategory(Guid categoryId, UpdateCategoryRequest updateCategoryRequest)
    {
        var response = await _client.PutAsJsonAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}", updateCategoryRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task ArchiveCategorySuccessfully()
    {
        //create category first
        var newCategory = new CreateCategoryRequest("Test Category", "Description of Test Category");

        var categoryId = await CreateCategory(newCategory);

        categoryId.ShouldNotBe(Guid.Empty);

        //archive
        await ArchiveCategory(categoryId);

        //get
        var category = await GetCategory(categoryId);

        category.ShouldNotBeNull();

        category!.Status.ShouldBe(CategoryStatus.IsArchived.ToString());
    }

    private async Task<Guid> CreateCategory(CreateCategoryRequest createCategoryRequest)
    {
        var createResponse = await _client.PostAsJsonAsync($"{UrlConstants.BaseCategoryURL}", createCategoryRequest);
        var categoryId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        createResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        return categoryId;
    }

    private async Task<CategoryResponse> GetCategory(Guid categoryId)
    {
        var getResponse = await _client.GetAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}");
        var category = await getResponse.Content.ReadFromJsonAsync<CategoryResponse>();

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        category.ShouldNotBeNull();

        return category;
    }

    private async Task ArchiveCategory(Guid categoryId)
    {
        var response = await _client.PutAsync($"{UrlConstants.BaseCategoryURL}/{categoryId}/{UrlConstants.SpecificArchiveCategoryURL}", null);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
