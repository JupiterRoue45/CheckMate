using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Address;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IAddressService
    {
        Task<Result<Address>> CreateAddressAsync(AddressCreationDto dto);

        Task<Result<Address>> GetAddressByIdAsync(int id);

        Task<IEnumerable<Address>> GetAddressesByLabel(string label);

        Task<IEnumerable<Address>> GetAllAddresses();

        Task<Result> DeleteAddressAsync(int id);

        Task<Result<Address>> UpdateAddressAsync(int id, AddressUpdateDto dto);
    }
}
