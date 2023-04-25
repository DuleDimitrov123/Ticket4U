using Common;
using Common.Constants;
using FluentValidation;
using Shows.Api.Requests.Performers;

namespace Shows.Api.RequestValidators.Performers;

public class CreatePerformerRequestValidator : AbstractValidator<CreatePerformerRequest>
{
    public CreatePerformerRequestValidator()
    {
        RuleFor(performer => performer.Name)
            .NotEmpty().WithMessage(DefaultErrorMessages.PerformerNameIsRequired);

        RuleFor(performer => performer.Name)
            .MaximumLength(PerformerConstants.PerfomerNameMaxLenght)
            .WithMessage(DefaultErrorMessages.PerformerNameLength);
    }
}
