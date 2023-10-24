using FluentValidation;
using Users.Common;

namespace Users.Api.Controllers.Requests;

public record AuthenticateUserRequest(string Email, string Password);

public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserEmailRequired);

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserPasswordRequired);
    }
}
