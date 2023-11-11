using MediatR;

namespace Shows.Application.Features.Shows.Commands.AddShowMessage;

public class AddShowMessageCommand : IRequest<Unit>
{
    public Guid ShowId { get; set; }

    public string ShowMessageName { get; set; }

    public string ShowMessageValue { get; set; }
}
