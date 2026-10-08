using CheckMate.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Errors.Address
{
    public static class AddressErrors
    {
        public static Error AddressNotFound(int id) => new()
        {
            Code = "Address.NotFound",
            Message = $"There is no such an adrress with the id {id}",
            Type = ErrorTypeEnum.NOT_FOUND
        };

        public static Error InexistantCountry() => new()
        {
            Code = "Address.InexistantCountry",
            Message = "The country you are trying to assign to the address does not exist.",
            Type = ErrorTypeEnum.NOT_FOUND
        };
    }
}
