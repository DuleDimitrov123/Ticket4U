using MediatR;

namespace Shows.Application.Features.Shows.Commands.UpdateShowPrice;

public class UpdateShowPriceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public decimal NewAmount { get; set; }
}
