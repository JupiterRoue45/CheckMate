using CheckMate.Application.DTOs.Address;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Address
{
    public class AddressUpdateValidator : AbstractValidator<AddressUpdateDto>
    {
        public AddressUpdateValidator()
        {
            RuleFor(x => x.Label)
                .NotEmpty().WithMessage("The Label field is required.")
                .MaximumLength(50).WithMessage("The Label field must not exceed 50 characters.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("The Street field is required.")
                .MaximumLength(100).WithMessage("The Street field must not exceed 100 characters.");

            RuleFor(x => x.Number)
                .MaximumLength(10).WithMessage("The Number field must not exceed 10 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("The City field is required.")
                .MaximumLength(50).WithMessage("The City field must not exceed 50 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("The State field is required.")
                .MaximumLength(50).WithMessage("The State field must not exceed 50 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("The ZipCode field is required.")
                .MaximumLength(20).WithMessage("The ZipCode field must not exceed 20 characters.");

            RuleFor(x => x.CountryId)
                .NotNull().WithMessage("The CountryId field is required.")
                .GreaterThan(0).WithMessage("The CountryId must be greater than 0.");
        }
    }
}
