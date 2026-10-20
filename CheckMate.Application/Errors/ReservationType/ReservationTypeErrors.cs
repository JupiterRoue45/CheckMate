using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.ReservationType
{
    public static class ReservationTypeErrors
    {
        public static Error NotFound(int id) => new()
        {
            Code = "ReservationType.NotFound",
            Message = $"There is no reservation type with the id {id}",
            Type = ErrorTypeEnum.NOT_FOUND
        };

        public static Error CodeAlreadyExists(string code) => new()
        {
            Code = "ReservationType.CodeAlreadyExists",
            Message = $"A reservation type with the code \"{code}\" already exists.",
            Type = ErrorTypeEnum.CONFLICT
        };
    }
}
