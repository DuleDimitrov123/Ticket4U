using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Requests.Categories;
using Shows.Application.Features.Categories.Commands;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Categories.Queries.GetCategories;
using Shows.Application.Features.Categories.Queries.GetCategoryById;

namespace Shows.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand()
        {
            Name = request.Name,
            Description = request.Description
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{categoryId}")]
    public async Task<ActionResult<CategoryResponse>> GetById([FromRoute] Guid categoryId)
    {
        var response = await _mediator.Send(new GetCategoryByIdQuery() { CategoryId = categoryId });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IList<CategoryResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetCategoriesQuery());

        return Ok(response);
    }
}
