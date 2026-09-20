using CheckMate.Application.Interfaces.RepositoryInterfaces;
using CheckMate.Application.Interfaces.ServiceInterfaces;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _CountryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _CountryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _CountryRepository.GetAllCountries();
        }
    }
}
