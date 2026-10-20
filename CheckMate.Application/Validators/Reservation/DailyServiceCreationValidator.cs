using CheckMate.Application.DTOs.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Reservation
{
    public class DailyServiceCreationValidator : AbstractValidator<DailyServiceCreationDto>
    {
        public DailyServiceCreationValidator()
        {
            RuleFor(x => x.ServiceId)
                .GreaterThan(0).WithMessage("The ServiceId field must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("The Quantity field must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("The UnitPrice field must be greater than 0");
        }
    }
}
