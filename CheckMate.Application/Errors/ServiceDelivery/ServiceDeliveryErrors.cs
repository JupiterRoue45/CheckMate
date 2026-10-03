using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.ServiceDelivery
{
    public static class ServiceDeliveryErrors
    {
        public static Error AlreadyExists(string name) => Error.Conflict(
            code: "ServiceDelivery.AlreadyExists",
            message: $"Service with name '{name}' already exists."
        );

        public static Error NotFound(int id) => Error.NotFound(
            code: "ServiceDelivery.NotFound",
            message: $"Service with ID {id} was not found."
        );
    }
}
