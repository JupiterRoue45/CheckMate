using CheckMate.Application.DTOs.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Validators.Reservation
{
    public class ReservationRoomCreationValidator : AbstractValidator<ReservationRoomCreationDto>
    {
        public ReservationRoomCreationValidator()
        {
            RuleFor(x => x.RoomTypeId)
                .GreaterThan(0).WithMessage("The RoomTypeId must be greater than 0");

            RuleFor(x => x.RoomId)
                .GreaterThan(0).When(x => x.RoomId is not null)
                .WithMessage("The field RoomId can not lower than 1");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("The Comment field can not exceed 500 characters");

            RuleFor(x => x.Adults)
                .GreaterThanOrEqualTo(1).WithMessage("There Must be atleast one adult in each room");

            RuleFor(x => x.Children)
                .GreaterThanOrEqualTo(0).WithMessage("The number of children can not be negative");

            RuleFor(x => x.Infants)
                .GreaterThanOrEqualTo(0).WithMessage("The number of infants can not be negative");

            RuleFor(x => x.CancellationPolicyId)
                .GreaterThan(0).WithMessage("The CancellationPolicyId must be greater than 0");

            RuleFor(x => x.Occupants)
                .NotEmpty().WithMessage("There should be at least 1 occupant per room");

            RuleFor(x => x.Services)
                .NotEmpty().WithMessage("There must be atleast one service per room");
        }
    }
}
