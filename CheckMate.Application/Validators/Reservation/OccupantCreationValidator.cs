using CheckMate.Application.DTOs.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Reservation
{
    public class OccupantCreationValidator : AbstractValidator<OccupantCreationDto>
    {
        public OccupantCreationValidator()
        {
            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("The PersonId field must be greater than 0");

            RuleFor(x => x.Start)
                .NotNull().WithMessage("The Start field is required.");

            RuleFor(x => x.End)
                .GreaterThan(x => x.Start)
                .When(x => x.End is not null)
                .WithMessage("The ending date must be greater than the starting one.");
        }
    }
}
