using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Common.Results
{
    /// <summary>
    /// Represents an error that can occur during the execution of an operation.
    /// </summary>
    /// <param name="Code"></param>
    /// <param name="Message"></param>
    /// <param name="Type"></param>
    public readonly record struct Error(string Code, string Message, ErrorTypeEnum Type)
    {
        public static Error None => new(string.Empty, string.Empty, ErrorTypeEnum.FAILURE);

        public static Error Validation(string code, string message) => new(code, message, ErrorTypeEnum.VALIDATION);

        public static Error NotFound(string code, string message) => new(code, message, ErrorTypeEnum.NOT_FOUND);

        public static Error Conflict(string code, string message) => new(code, message, ErrorTypeEnum.CONFLICT);

        public static Error Forbidden(string code, string message) => new(code, message, ErrorTypeEnum.FORBIDDEN);
    }
}
