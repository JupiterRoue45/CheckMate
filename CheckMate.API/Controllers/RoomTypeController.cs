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
            RoomType? type = await _roomTypeService.GetRoomType(Id);

            if (type is null)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    instance: Request.Path,
                    title: "Non existant element",
                    type: "",
                    detail:$"There is no RoomType with the id : {Id}"
                    );
            }

            return Ok(type);
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
            throw new NotImplementedException();
        }
    }
}
