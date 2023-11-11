using FluentValidation;
using Reservations.Common;
using Reservations.Common.Constants;

namespace Reservations.Api.Requests.Reservations;

public record UpdateNumberOfReservationsRequest(int NewNumberOfReservations);

public class UpdateNumberOfReservationsRequestValidator : AbstractValidator<UpdateNumberOfReservationsRequest>
{
    public UpdateNumberOfReservationsRequestValidator()
    {
        RuleFor(request => request.NewNumberOfReservations)
            .GreaterThan(0)
            .WithMessage(DefaultErrorMessages.NumberOfReservationsGreaterThan0)
            .LessThanOrEqualTo(ReservationConstants.MaxNumberOfReservationsPerUser)
            .WithMessage($"{DefaultErrorMessages.NumberOfReservationsGreaterThanLimit} {ReservationConstants.MaxNumberOfReservationsPerUser}.");
    }
}