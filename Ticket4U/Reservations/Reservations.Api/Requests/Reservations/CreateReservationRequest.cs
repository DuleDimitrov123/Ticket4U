using FluentValidation;
using Reservations.Common;
using Reservations.Common.Constants;

namespace Reservations.Api.Requests.Reservations;

public record CreateReservationRequest(Guid ExternalUserId, Guid ExternalShowId, int NumberOfReservations);

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(request => request.ExternalUserId)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserIdForReservationRequired);

        RuleFor(request => request.ExternalShowId)
            .NotEmpty().WithMessage(DefaultErrorMessages.ShowIdForReservationRequired);

        RuleFor(request => request.NumberOfReservations)
            .GreaterThan(0)
            .WithMessage(DefaultErrorMessages.NumberOfReservationsGreaterThan0)
            .LessThan(ReservationConstants.MaxNumberOfReservationsPerUser)
            .WithMessage($"{DefaultErrorMessages.NumberOfReservationsGreaterThanLimit} {ReservationConstants.MaxNumberOfReservationsPerUser}.");
    }
}