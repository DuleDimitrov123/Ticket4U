﻿using FluentValidation;
using Reservations.Common;

namespace Reservations.Api.Requests;

/// <summary>
/// Validator for validating API requests that accept guid and not allowing empty guid.
/// </summary>
public class EmptyGuidValidator : AbstractValidator<Guid>
{
    public EmptyGuidValidator()
    {
        RuleFor(guid => guid).NotEmpty().WithMessage(DefaultErrorMessages.CantGetByEmptyGuid);
    }
}
