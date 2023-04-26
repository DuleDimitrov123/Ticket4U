using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Requests.Categories;
using Shows.Application.Features.Categories.Commands.ArchiveCategory;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.Application.Features.Categories.Commands.UpdateCategory;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryResponse>> GetById([FromRoute] Guid categoryId)
    {
        var response = await _mediator.Send(new GetCategoryByIdQuery() { CategoryId = categoryId });

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<CategoryResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetCategoriesQuery());

        return Ok(response);
    }

    [HttpPut("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand()
        {
            CategoryId = categoryId,
            NewName = request.NewName,
            NewDescription = request.NewDescription
        };

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPut("{categoryId}/archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ArchiveCategory([FromRoute] Guid categoryId)
    {
        await _mediator.Send(new ArchiveCategoryCommand() { CategoryId = categoryId });

        return NoContent();
    }
}
