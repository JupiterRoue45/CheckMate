using CheckMate.Application.DTOs.ReservationType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.ReservationType
{
    public class ReservationTypeCreationValidator : AbstractValidator<ReservationTypeCreationDto>
    {
        public ReservationTypeCreationValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("The Code field is required.")
                .MaximumLength(20).WithMessage("The Code field must not exceed 20 characters");

            RuleFor(x => x.Description)
                .MaximumLength(256).WithMessage("The Description field must not exceed 256 characters");
        }
    }
}
