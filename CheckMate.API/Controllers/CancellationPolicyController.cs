using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.CancellationPolicy;
using CheckMate.Application.Errors.CancellationPolicy;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/cancellation-policies")]
    [ApiController]
    public class CancellationPolicyController : ControllerBase
    {
        private readonly ICancellationPolicyService _cancellationPolicyService;

        public CancellationPolicyController(ICancellationPolicyService cancellationPolicyService)
        {
            _cancellationPolicyService = cancellationPolicyService;
        }

        [HttpGet]
        public async Task<ActionResult<CancellationPolicy>> GetAll()
        {
            return Ok(await _cancellationPolicyService.GetCancellationPoliciesAsync());
        }

        [HttpGet("search")]
        public async Task<ActionResult<CancellationPolicy>> GetByCode([FromQuery] string code)
        {
            return Ok(await _cancellationPolicyService.GetCancellationPoliciesByCodeAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CancellationPolicyCreationDto requestDto)
        {
            Result<CancellationPolicy> result = await _cancellationPolicyService.CreateCancellationPolicyAsync(requestDto);

            if (result.IsFailure)
                return result.Error.Value.ToActionResult(this);
            else
                return Ok(result.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CancellationPolicyUpdateDto requestDto)
        {
            Result<CancellationPolicy> result = await _cancellationPolicyService.UpdateCancellationPolicyAsync(id, requestDto);

            if (result.IsFailure)
                return result.Error.Value.ToActionResult(this);
            else
                return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Result<CancellationPolicy> result = await _cancellationPolicyService.GetCancellationPolicyByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return result.Error.Value.ToActionResult(this);
            }
        }
    }
}
