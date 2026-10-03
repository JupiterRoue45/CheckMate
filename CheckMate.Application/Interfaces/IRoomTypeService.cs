using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomType;
using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IRoomTypeService
    {
        Task<Result<RoomType>> GetRoomType(int Id);
        Task<Result<RoomType>> CreateRoomType(RoomTypeCreationDto dto);
        Task<IEnumerable<RoomType>> GetAllRoomTypes();
    }
}
