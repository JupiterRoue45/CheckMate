using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Application.DTOs.Company;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Result<ClientDto>> GetCompanyByIdAsync(int Id);

        Task<IEnumerable<ClientDto>> ListAllCompaniesAsync();

        Task<Result<Company>> CreateCompanyAsync(CompanyCreationDto dto);
    }
}
