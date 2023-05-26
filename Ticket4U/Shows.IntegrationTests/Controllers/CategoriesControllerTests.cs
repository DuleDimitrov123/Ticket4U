using Shouldly;
using Shows.Api.Requests.Categories;
using Shows.Application.Features.Categories.Queries;
using Shows.Domain.Categories;
using Shows.IntegrationTests.Base;
using Shows.IntegrationTests.Constants;
using System.Net;
using System.Text.Json;

namespace Shows.IntegrationTests.Controllers;

public class CategoriesControllerTests : BaseControllerTests
{
    public CategoriesControllerTests(CustomWebApplicationFactory<Program> factory)
        :base(factory)
    {
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
    public async Task GetCategoryByIdNotFound()
    {
        var response = await _client.GetAsync($"{UrlConstants.BaseCategoryURL}/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCategoryWithEmptyGuid()
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
}
