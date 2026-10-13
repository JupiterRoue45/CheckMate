using CheckMate.Application.Errors.CancellationPolicy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.CancellationPolicy
{
    public class CancellationPolicyUpdateValidator : AbstractValidator<CancellationPolicyUpdateDto>
    {
        public CancellationPolicyUpdateValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("The Code field is required.")
                .Length(2, 50).WithMessage("The Code length must be between 2 and 50 characters.");

            RuleFor(x => x.DeadlineHours)
                .GreaterThan(0).WithMessage("Can not assign a negative or 0 to the deadline hours.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The Description field must not exceed 500 characters");
        }
    }
}
