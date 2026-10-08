using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Client;
using CheckMate.Application.DTOs.Company;
using CheckMate.Application.Errors.Company;
using CheckMate.Application.Helpers;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;

namespace CheckMate.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
        {
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<Result<Company>> CreateCompanyAsync(CompanyCreationDto dto)
        {
           Company company = new()
           {
               CompanyName = dto.CompanyName,
               TVA = dto.TVA,
               PhoneAreaCode = dto.PhoneAreaCode,
               PhoneNumber = dto.PhoneNumber,
               Email = dto.Email
           };
            await _companyRepository.CreateAsync(company);

            await _unitOfWork.SaveChangesAsync();

            return Result<Company>.Success(company);
        }

        public async Task<Result<ClientDto>> GetCompanyByIdAsync(int Id)
        {
            Company? company = await _companyRepository.GetByIdAsync(Id);

            if (company is null)
            {
                return Result<ClientDto>.Failure(CompanyErrors.CompanyNotFound(Id));
            }

            return Result<ClientDto>.Success(company.ToDto());
        }

        public async Task<IEnumerable<ClientDto>> ListAllCompaniesAsync()
        {
            IEnumerable<Company> companies = await _companyRepository.GetAllAsync();
            return companies.Select(c => c.ToDto());
        }
    }
}
