using MediatR;

namespace Shows.Application.Performers.Queries.GetPerformerById;

public class GetPerformerByIdQuery : IRequest<PerformerResponse>
{
    public Guid Id { get; set; }
}
