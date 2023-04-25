using MediatR;

namespace Shows.Application.Features.Performers.Queries.GetPerformerById;

public class GetPerformerByIdQuery : IRequest<PerformerResponse>
{
    public Guid Id { get; set; }
}
