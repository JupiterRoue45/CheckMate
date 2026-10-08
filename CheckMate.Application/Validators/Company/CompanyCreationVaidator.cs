using CheckMate.Application.DTOs.Company;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Company
{
    public class CompanyCreationVaidator : AbstractValidator<CompanyCreationDto>
    {
        public CompanyCreationVaidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            RuleFor(x => x.TVA)
                .MaximumLength(20).WithMessage("TVA must be at most 20 characters.");

            RuleFor(x => x.PhoneAreaCode)
                .MaximumLength(8).WithMessage("Phone area code cannot exceed 8 characters.")
                .MinimumLength(2).When(x => x.PhoneAreaCode is not null).WithMessage("Phone area code must be between 2 and 8 characters.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.")
                .Null().When(x => string.IsNullOrEmpty(x.PhoneAreaCode)).WithMessage("Phone number must be null when phone area code is not provided.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
