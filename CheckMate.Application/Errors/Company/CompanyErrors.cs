using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.Company
{
    public static class CompanyErrors
    {
        public static Error CompanyNotFound(int id) => new()
        {
            Code = "Company.NotFound",
            Message = $"Company with ID {id} was not found.",
            Type = ErrorTypeEnum.NOT_FOUND
        };
    }
}
