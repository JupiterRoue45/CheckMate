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
    }
}
