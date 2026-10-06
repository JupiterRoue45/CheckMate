using CheckMate.Application.DTOs.Room;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Room
{
    public class RoomRegistrationValidator : AbstractValidator<RoomCreationDto>
    {
        public RoomRegistrationValidator()
        {
            RuleFor(r => r.Number)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Room number is required.")
                .MinimumLength(1).WithMessage("Room number must be at least 1 character long.");

            RuleFor(r => r.RoomTypeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Room type is required and must be a positive integer.");

            RuleFor(r => r.Area)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).When(r => r.Area.HasValue).WithMessage("Area must be greater than 0.");
        }
    }
}
