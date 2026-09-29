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
            Room? room = await _roomService.GetRoom(Id);

            if (room is null)
            {
                return Problem(
                    title: "Non existant element",
                    detail: $"There is no room with the number : {Id}",
                    instance: Request.Path,
                    statusCode: StatusCodes.Status404NotFound,
                    type: ""
                    );
            }

            return Ok(room);
        }
    }
}
