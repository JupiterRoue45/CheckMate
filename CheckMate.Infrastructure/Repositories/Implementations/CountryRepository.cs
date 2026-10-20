using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CheckMateDbContext _context;

        public CountryRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return  await _context.Countries
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync() ;
        }

        public async Task<bool> VerifyCountryExistance(int id)
        {
            return await _context.Countries.AnyAsync(c => c.CountryId == id);
        }

        public async Task<bool> VerifyPhoneAreaCodeExistence(string code)
        {
            IList<String> codes = await _context.Countries
                .AsNoTracking()
                .Select(c => c.PhoneDialCode)
                .ToListAsync();

            return codes
                .Select(c => c.Split(',', StringSplitOptions.TrimEntries))
                .Any(c => c.Contains(code));
        }
    }
}
