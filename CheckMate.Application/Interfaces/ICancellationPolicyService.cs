using CheckMate.Application.Common.Results;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface ICancellationPolicyService
    {
        Task<Result<CancellationPolicy>> CreateCancellationPolicyAsync();

        Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesAsync();

        Task<IEnumerable<CancellationPolicy>> GetCancellationPoliciesByCodeAsync(string code);
    }
}
