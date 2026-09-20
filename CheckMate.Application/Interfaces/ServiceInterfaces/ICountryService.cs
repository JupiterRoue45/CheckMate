using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces.ServiceInterfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountries();
    }
}
