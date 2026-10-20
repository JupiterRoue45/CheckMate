using CheckMate.Application.DTOs.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Reservation
{
    public class ReservationCreationValidator : AbstractValidator<ReservationCreationDto>
    {
        public ReservationCreationValidator()
        {
            RuleFor(x => x.Arrival)
                .NotNull().WithMessage("The Arrival field is required.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)))
                .WithMessage("Can not insert a booking for a passed day.");

            RuleFor(x => x.Departure)
                .NotNull().WithMessage("The Departure field is required")
                .GreaterThan(x => x.Arrival)
                .WithMessage("The Checkout date must be superior to the Checkin date.");

            RuleFor(x => x.ReservationType)
                .GreaterThan(0).WithMessage("The ReservationType field must be greater than 0");

            RuleFor(x => x.BookerId)
                .GreaterThan(0).WithMessage("The BookerId field must be greater than 0");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("The Number field is required.")
                .Length(10, 100).WithMessage("The Number field must have between 10 and 100 characters");

            RuleFor(x => x.Voucher)
                .MaximumLength(50).WithMessage("The Voucher field can not exceed 50 characters");

            RuleFor(x => x.rooms)
                .NotEmpty().WithMessage("There should be atleast one room per reservation.");
        }
    }
}
