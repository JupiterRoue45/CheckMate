using CheckMate.Application.Common.Results;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Result<Company>> GetCompanyByIdAsync(int Id);

        Task<IEnumerable<Company>> ListAllCompaniesAsync();

        Task<Result<Company>> CreateCompanyAsync();
    }
}
