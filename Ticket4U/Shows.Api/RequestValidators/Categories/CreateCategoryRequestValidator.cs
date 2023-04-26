using Common;
using Common.Constants;
using FluentValidation;
using Shows.Api.Requests.Categories;

namespace Shows.Api.RequestValidators.Categories;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage(DefaultErrorMessages.CategoryNameIsRequired);

        RuleFor(request => request.Name)
            .MaximumLength(CategoryConstants.CategoryNameMaxLength)
            .WithMessage(DefaultErrorMessages.CategoryNameLength);

        RuleFor(request => request.Description)
            .NotEmpty().WithMessage(DefaultErrorMessages.CategoryDescriptionIsRequired);
    }
}
