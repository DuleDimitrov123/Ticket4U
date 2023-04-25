using Common;
using FluentValidation;
using Shows.Api.Requests.Performers;

namespace Shows.Api.RequestValidators.Performers;

public class CreatePerformerInfoRequestValidator : AbstractValidator<PerformerInfoRequest>
{
    public CreatePerformerInfoRequestValidator()
    {
        RuleFor(performerInfo => performerInfo.Name)
            .NotEmpty().WithMessage(DefaultErrorMessages.PerformerInfoNameRequired);

        RuleFor(performerInfo => performerInfo.Value)
            .NotEmpty().WithMessage(DefaultErrorMessages.PerformerInfoValueRequired);
    }
}
