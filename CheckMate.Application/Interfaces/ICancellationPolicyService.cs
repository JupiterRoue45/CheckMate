using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.CancellationPolicy;
using CheckMate.Application.Errors.CancellationPolicy;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface ICancellationPolicyService
    {
        Task<Result<CancellationPolicy>> CreateCancellationPolicyAsync(CancellationPolicyCreationDto dto);

        Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesAsync();

        Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesByCodeAsync(string code);

        Task<Result<CancellationPolicy>> UpdateCancellationPolicyAsync(int id, CancellationPolicyUpdateDto dto);

        Task<Result> DeleteCancellationPolicyAsync(int id);

        Task<Result<CancellationPolicy>> GetCancellationPolicyByIdAsync(int id);
    }
}
