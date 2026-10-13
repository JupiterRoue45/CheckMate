using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.CancellationPolicy;
using CheckMate.Application.Errors.CancellationPolicy;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Services
{
    public class CancellationPolicyService : ICancellationPolicyService
    {
        private readonly ICancellationPolicyRepository _cancellationPolicyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancellationPolicyService(
            ICancellationPolicyRepository cancellationPolicyRepository,
            IUnitOfWork unitOfWork)
        {
            _cancellationPolicyRepository = cancellationPolicyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CancellationPolicy>> CreateCancellationPolicyAsync(CancellationPolicyCreationDto dto)
        {
            if (await _cancellationPolicyRepository.VerifyExistenceOfCode(dto.Code))
                return Result<CancellationPolicy>.Failure(CancellationPolicyErrors.CodeAlreadyExists(dto.Code));

            CancellationPolicy cancellationPolicy = new()
            {
                Code = dto.Code,
                Description = dto.Description,
                DeadlineHours = dto.DeadlineHours
            };

            await _cancellationPolicyRepository.CreateAsync(cancellationPolicy);

            await _unitOfWork.SaveChangesAsync();

            return Result<CancellationPolicy>.Success(cancellationPolicy);
        }

        public Task<Result> DeleteCancellationPolicyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesAsync()
        {
            return await _cancellationPolicyRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesByCodeAsync(string code)
        {
            return await _cancellationPolicyRepository.GetCancellationPoliciesByCodeAsync(code);
        }

        public async Task<Result<CancellationPolicy>> GetCancellationPolicyByIdAsync(int id)
        {
            CancellationPolicy? cancellationPolicy = await _cancellationPolicyRepository.GetAsync(id);

            if (cancellationPolicy is null)
                return Result<CancellationPolicy>.Failure(CancellationPolicyErrors.CancellationPolicyNotFound(id));
            else
                return Result<CancellationPolicy>.Success(cancellationPolicy);
        }

        public async Task<Result<CancellationPolicy>> UpdateCancellationPolicyAsync(int id, CancellationPolicyUpdateDto dto)
        {
            CancellationPolicy? cancellationPolicy = await _cancellationPolicyRepository.GetAsync(id);

            if (cancellationPolicy is null)
                return Result<CancellationPolicy>.Failure(CancellationPolicyErrors.CancellationPolicyNotFound(id));

            if (cancellationPolicy.Code != dto.Code
                && await _cancellationPolicyRepository.VerifyExistenceOfCode(dto.Code))
                return Result<CancellationPolicy>.Failure(CancellationPolicyErrors.CodeAlreadyExists(dto.Code));

            cancellationPolicy.Code = dto.Code;
            cancellationPolicy.DeadlineHours = dto.DeadlineHours;
            cancellationPolicy.Description = dto.Description;

            await _unitOfWork.SaveChangesAsync();

            return Result<CancellationPolicy>.Success(cancellationPolicy);
        }
    }
}
