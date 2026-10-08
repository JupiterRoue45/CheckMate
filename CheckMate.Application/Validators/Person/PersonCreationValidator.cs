using CheckMate.Application.DTOs.Person;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Person
{
    public class PersonCreationValidator : AbstractValidator<PersonCreationDto>
    {
        public PersonCreationValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.IsAdult)
                .NotNull().WithMessage("IsAdult field is required.");

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
