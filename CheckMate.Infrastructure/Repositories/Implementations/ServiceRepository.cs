using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly CheckMateDbContext _context;

        public ServiceRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.AsNoTracking().ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int Id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == Id);
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
        }

        public async Task<bool> VerifyExistanceByNameAsync(string serviceName)
        {
            return await _context.Services.AnyAsync(s => s.ServiceName == serviceName);
            
        }
    }
}
