using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomType;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/room-types")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(
            IRoomTypeService roomTypeService
            )
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int Id)
        {
            Result<RoomType> result = await _roomTypeService.GetRoomType(Id);

            if (result.IsFailure)
            {
                return result.Error.Value.ToActionResult(this);
            }
            else
            {
                return Ok(result.Value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomTypeCreationDto requestDto)
        {
            RoomType createdRoomType = await _roomTypeService.CreateRoomType(requestDto);

            return CreatedAtAction(
                nameof(Get),
                new { Id = createdRoomType.RoomTypeId},
                createdRoomType
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<RoomType> roomTypes = await _roomTypeService.GetAllRoomTypes();

            return Ok(roomTypes);
        }
    }
}
