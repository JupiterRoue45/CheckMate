using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.CancellationPolicy
{
    public static class CancellationPolicyErrors
    {
        public static Error CodeAlreadyExists(string code) => new Error()
        {
            Code = "CancellationPolicy.DuplicateCode",
            Message = $"A cancellation policy with the code : \"{code}\" already exists",
            Type = ErrorTypeEnum.CONFLICT
        };

        public static Error CancellationPolicyNotFound(int id) => new()
        {
            Code = "CancellationPolicy.NotFound",
            Message = $"There is no Cancellation Policy with the id {id}",
            Type = ErrorTypeEnum.NOT_FOUND
        };
    }
}
