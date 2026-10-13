using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface ICancellationPolicyRepository
    {
        Task CreateAsync(CancellationPolicy cancellationPolicy);
        Task<IEnumerable<CancellationPolicy>> GetAllAsync();
        Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesByCodeAsync(string code);
        Task<bool> VerifyExistenceOfCode(string code);
        Task<CancellationPolicy?> GetAsync(int id);
    }
}
