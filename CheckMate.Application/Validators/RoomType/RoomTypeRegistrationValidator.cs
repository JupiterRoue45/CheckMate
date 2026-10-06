using CheckMate.Application.DTOs.RoomType;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.RoomType
{
    public class RoomTypeRegistrationValidator : AbstractValidator<RoomTypeCreationDto>
    {
        public RoomTypeRegistrationValidator()
        { 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Room type name is required.")
                .MaximumLength(50).WithMessage("Room type name should not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Rank)
                .GreaterThan(0).WithMessage("Rank must be a positive integer.");

            RuleFor(x => x.MaxOccupancy)
                .GreaterThan(0).WithMessage("Max occupancy must be a positive integer.");
        }
    }
}
