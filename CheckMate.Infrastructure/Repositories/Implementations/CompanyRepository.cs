using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CheckMateDbContext _context;

        public CompanyRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int Id)
        {
            return await _context.Companies.FindAsync(Id);
        }
    }
}
