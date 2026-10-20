using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.ReservationType;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/reservation-types")]
    [ApiController]
    public class ReservationTypeController : ControllerBase
    {
        private readonly IReservationTypeService _reservationTypeService;
        public ReservationTypeController(IReservationTypeService reservationTypeService)
        {
            _reservationTypeService = reservationTypeService;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            Result<ReservationType> result = await _reservationTypeService.GetReservationTypeByIdAsync(id);
            
            if (result.IsSuccess)
                return Ok(result.Value);

            else
                return result.Error.Value.ToActionResult(this);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reservationTypeService.GetAllReservationTypesAsync());
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            return Ok(await _reservationTypeService.GetAllReservationTypesFromCodeAsync(code));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReservationTypeCreationDto requestDto)
        {
            Result<ReservationType> result = await _reservationTypeService.CreateReservationTypeAsync(requestDto);

            if (result.IsSuccess)
                return Ok(result.Value);

            else
                return result.Error.Value.ToActionResult(this);
        }
    }
}
