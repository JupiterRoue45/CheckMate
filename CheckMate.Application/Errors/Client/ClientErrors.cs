using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.Client
{
    public static class ClientErrors
    {
        public static Error CLientNotFound(int id) => new()
        {
            Code = "Client.NotFound",
            Message = $"Client with ID {id} not found.",
            Type = ErrorTypeEnum.NOT_FOUND
        };
    }
}
