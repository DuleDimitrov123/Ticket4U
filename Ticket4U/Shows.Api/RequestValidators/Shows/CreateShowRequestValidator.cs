using Common;
using Common.Constants;
using FluentValidation;
using Shows.Api.Requests.Shows;

namespace Shows.Api.RequestValidators.Shows;

public class CreateShowRequestValidator : AbstractValidator<CreateShowRequest>
{
    public CreateShowRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(DefaultErrorMessages.ShowNameIsRequired);

        RuleFor(request => request.Name)
            .MaximumLength(ShowConstants.ShowNameMaxLenght)
            .WithMessage(DefaultErrorMessages.ShowNameLength);

        RuleFor(request => request.Description)
            .NotEmpty()
            .WithMessage(DefaultErrorMessages.ShowDescriptionIsRequired);

        RuleFor(request => request.Description)
            .MaximumLength(ShowConstants.ShowDescriptionMaxLenght)
            .WithMessage(DefaultErrorMessages.ShowDescriptionLength);

        RuleFor(request => request.Location)
            .NotEmpty()
            .WithMessage(DefaultErrorMessages.ShowLocationIsRequired);

        RuleFor(request => request.NumberOfplaces)
            .GreaterThan(0)
            .WithMessage(DefaultErrorMessages.NumberOfPlacesGreaterThan0);

        RuleFor(request => request.TicketPriceCurrency)
            .NotEmpty()
            .WithMessage(DefaultErrorMessages.MoneyCurrencyRequired);

        RuleFor(request => request.TicketPriceCurrency)
            .Length(ShowConstants.MoneyCurrencyExactLength)
            .WithMessage(DefaultErrorMessages.MoneyCurrencyLength);

        RuleFor(request => request.TickerPriceAmount)
            .GreaterThan(-1)
            .WithMessage(DefaultErrorMessages.MoneyAmountNotNegative);

        RuleFor(request => request.PerformerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage(DefaultErrorMessages.ShowPerformerIdRequired);

        RuleFor(request => request.CategoryId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage(DefaultErrorMessages.ShowCategoryIdRequired);
    }
}
