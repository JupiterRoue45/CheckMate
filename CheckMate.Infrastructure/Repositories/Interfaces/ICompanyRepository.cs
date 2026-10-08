using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task CreateAsync(Company company);

        Task<Company?> GetByIdAsync(int Id);

        Task<IEnumerable<Company>> GetAllAsync();
    }
}
