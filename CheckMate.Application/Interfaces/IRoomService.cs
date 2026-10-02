using CheckMate.Application.DTOs.Room;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();

        Task<Room?> GetRoom(int Id);

        Task<Room> CreateRoom(RoomCreationDto dto);
    }
}
