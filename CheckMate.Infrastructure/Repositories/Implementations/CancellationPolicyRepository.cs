using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class CancellationPolicyRepository : ICancellationPolicyRepository
    {
        private readonly CheckMateDbContext _context;

        public CancellationPolicyRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(CancellationPolicy cancellationPolicy)
        {
            await _context.CancellationPolicies.AddAsync(cancellationPolicy);
        }

        public async Task<IEnumerable<CancellationPolicy>> GetAllAsync()
        {
            return await _context.CancellationPolicies
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesByCodeAsync(string code)
        {
            return await _context.CancellationPolicies
                .Where(cp => cp.Code.Contains(code))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
