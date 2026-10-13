using CheckMate.Application.DTOs.RoomUnavailability;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.RoomUnavailability
{
    public class RoomUnavailabilityCreationValidator : AbstractValidator<RoomUnavailabilityCreationDto>
    {
        public RoomUnavailabilityCreationValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("The RoomNumber field is required");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("The Reason field is required.")
                .MaximumLength(256).WithMessage("The Reason field must not exceed 256 characters.");

            RuleFor(x => x.Comment)
                .MaximumLength(256).WithMessage("The Comment field must not exceed 256 characters");

            RuleFor(x => x.StartingDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Can not insert a booking for a previous date.");

            RuleFor(x => x.EndingDate)
                .GreaterThan(x => x.StartingDate).WithMessage("Impossible to have the checkout date inferior to the check-in date.");
        }
    }
}
