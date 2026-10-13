using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomUnavailability;
using CheckMate.Application.Interfaces;
using CheckMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/rooms-unavailabilities")]
    [ApiController]
    public class RoomUnvailabilityController : ControllerBase
    {
        private readonly IRoomUnavailabilityService _roomUnavailabilityService;
        private readonly UserManager<User> _userManager;

        public RoomUnvailabilityController(
            IRoomUnavailabilityService roomUnavailabilityService,
            UserManager<User> userManager)
        {
            _roomUnavailabilityService = roomUnavailabilityService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] RoomUnavailabilityCreationDto requestDto)
        {
            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
                return Unauthorized();

            Result<RoomUnavailabilityDto> result = await _roomUnavailabilityService.CreateRoomUnavailabilityAsync(requestDto, user);

            if (result.IsFailure)
                return result.Error.Value.ToActionResult(this);

            else
                return Ok(result.Value);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUnavailabilitiesBetweenStartAndEnd(DateOnly startingDate, DateOnly endingDate)
        {
            return Ok(await _roomUnavailabilityService.GetUnavailableRoomsBetweenTwoDates(startingDate, endingDate));
        }
        
    }
}
