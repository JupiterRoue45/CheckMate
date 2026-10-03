using CheckMate.API.Helpers;
using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.Room;
using CheckMate.Application.Interfaces;
using CheckMate.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckMate.API.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(
            IRoomService roomService
            )
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Room> rooms = await _roomService.GetAllRooms();

            return Ok(rooms);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            Result<Room> result = await  _roomService.GetRoom(Id);

            if (result.IsFailure)
            {
                return result.Error.Value.ToActionResult(this);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomCreationDto requestDto)
        {
            Result<Room> result = await _roomService.CreateRoom(requestDto);

            if (result.IsFailure)
            {
                return result.Error.Value.ToActionResult(this);
            }

            return CreatedAtAction(nameof(Get), new { Id = result.Value.RoomId }, result.Value);
        }
    }
}
