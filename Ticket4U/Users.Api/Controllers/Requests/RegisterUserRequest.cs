using FluentValidation;
using Users.Common;

namespace Users.Api.Controllers.Requests;

public record RegisterUserRequest(string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password);

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserFirstNameRequired);

        RuleFor(request => request.LastName)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserLastNameRequired);

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserEmailRequired);

        RuleFor(request => request.UserName)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserUsernameRequired);

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserPasswordRequired);
    }
}
