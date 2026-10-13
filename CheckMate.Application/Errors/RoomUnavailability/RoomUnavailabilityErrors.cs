using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.RoomUnavailability
{
    public static class RoomUnavailabilityErrors
    {
        public static Error WrongDates() => new()
        {
            Code = "RoomUnavailability.WrongDates",
            Message = "The starting date is superior to the ending date.",
            Type = ErrorTypeEnum.VALIDATION
        };
    }
}
