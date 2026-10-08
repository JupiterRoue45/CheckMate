using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.Person
{
    public static class PersonErrors
    {
        public static Error PersonNotFound(int id) => new(
            Code: "Person.NotFound",
            Message: $"Person with ID {id} was not found.",
            Type: ErrorTypeEnum.NOT_FOUND
        );
    }
}
