using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address?> GetByIdAsync(int id);

        Task<IEnumerable<Address>> GetByLabelAsync(string label);

        Task<IEnumerable<Address>> GetAllAsync();

        Task CreateAsync(Address address);

        Task<bool> DeleteAsync(int id);

        Task<bool> VerifyLabelExistance(string label);
    }
}
