using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int Id);
        Task CreateAsync(Service service);
        Task<bool> VerifyExistanceByNameAsync(string serviceName);
        Task UpdateAsync(Service service);
    }
}
