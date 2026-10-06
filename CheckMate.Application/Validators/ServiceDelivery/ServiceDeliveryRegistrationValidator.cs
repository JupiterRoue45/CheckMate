using CheckMate.Application.DTOs.ServiceDelivery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.ServiceDelivery
{
    public class ServiceDeliveryRegistrationValidator : AbstractValidator<ServiceDeliveryCreationDto>
    {
        public ServiceDeliveryRegistrationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service delivery name is required.")
                .Length(1, 100).WithMessage("Service delivery name must be between 1 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("Unit price is required.")
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
