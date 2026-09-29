using CheckMate.Application.DTOs.RoomType;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IRoomTypeService
    {
        Task<RoomType?> GetRoomType(int Id);
        Task<RoomType> CreateRoomType(RoomTypeCreationDto dto);
    }
}
