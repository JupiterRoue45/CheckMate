using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces.RepositoryInterfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountries();
    }
}
