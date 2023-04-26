using Common.Constants;
using Common;
using FluentValidation;
using Shows.Api.Requests.Categories;

namespace Shows.Api.RequestValidators.Categories;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(request => request.NewName)
            .MaximumLength(CategoryConstants.CategoryNameMaxLength)
            .WithMessage(DefaultErrorMessages.CategoryNameLength)
            .When(request => !string.IsNullOrEmpty(request.NewName));
    }
}
