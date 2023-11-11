using FluentValidation;
using Reservations.Common;
using Reservations.Common.Constants;

namespace Reservations.Api.Requests.Reservations;

public record CreateReservationRequest(Guid UserId, Guid ShowId, int NumberOfReservations);

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty().WithMessage(DefaultErrorMessages.UserIdForReservationRequired);

        RuleFor(request => request.ShowId)
            .NotEmpty().WithMessage(DefaultErrorMessages.ShowIdForReservationRequired);

        RuleFor(request => request.NumberOfReservations)
            .GreaterThan(0)
            .WithMessage(DefaultErrorMessages.NumberOfReservationsGreaterThan0)
            .LessThan(ReservationConstants.MaxNumberOfReservationsPerUser)
            .WithMessage($"{DefaultErrorMessages.NumberOfReservationsGreaterThanLimit} {ReservationConstants.MaxNumberOfReservationsPerUser}.");
    }
}