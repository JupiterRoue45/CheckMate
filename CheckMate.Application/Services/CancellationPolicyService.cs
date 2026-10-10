using CheckMate.Application.Common.Results;
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

        public Task<Result<CancellationPolicy>> CreateCancellationPolicyAsync()
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
    }
}
