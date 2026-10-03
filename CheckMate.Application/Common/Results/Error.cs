using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Common.Results
{
    public readonly struct Error(string Code, string Message, ErrorTypeEnum Type)
    {
        public static Error None => new(string.Empty, string.Empty, ErrorTypeEnum.FAILURE);

        public static Error Validation(string code, string message) => new(code, message, ErrorTypeEnum.VALIDATION);

        public static Error NotFound(string code, string message) => new(code, message, ErrorTypeEnum.NOT_FOUND);

        public static Error Conflict(string code, string message) => new(code, message, ErrorTypeEnum.CONFLICT);

        public static Error Forbidden(string code, string message) => new(code, message, ErrorTypeEnum.FORBIDDEN);
    }
}
