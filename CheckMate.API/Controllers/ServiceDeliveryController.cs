using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceDeliveryController : ControllerBase
    {
        private readonly IServiceDeliveryService _serviceDeliveryService;

        public ServiceDeliveryController(IServiceDeliveryService serviceDeliveryService)
        {
            _serviceDeliveryService = serviceDeliveryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceDeliveryCreationDto dto)
        {
            Result<Service> result = await _serviceDeliveryService.CreateServiceAsync(dto);

            if (result.IsFailure)
            {
                return result.Error.Value.ToActionResult(this);
            }
            return CreatedAtAction(nameof(Get), new { id = result.Value.ServiceId }, result.Value);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            Result<Service> result = await _serviceDeliveryService.GetServiceByIdAsync(Id);

            if (result.IsFailure)
            {
                return result.Error.Value.ToActionResult(this);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Service> services = await _serviceDeliveryService.GetAllServices();

            return Ok(services);
        }
    }
}
