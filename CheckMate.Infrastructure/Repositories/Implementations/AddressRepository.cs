using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CheckMateDbContext _context;

        public AddressRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.AsNoTracking().ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<IEnumerable<Address>> GetByLabelAsync(string label)
        {
            return await _context.Addresses.
                Where(a => a.AddressLabel.Contains(label))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Address? address = await _context.Addresses.FindAsync(id);

            if (address != null)
            {
                _context.Addresses.Remove(address);
                return true;
            }

            return false;
        }

        public async Task<bool> VerifyLabelExistance(string label)
        {
            return await _context.Addresses.AnyAsync(a => string.Equals(a.AddressLabel, label));
        }
    }
}
