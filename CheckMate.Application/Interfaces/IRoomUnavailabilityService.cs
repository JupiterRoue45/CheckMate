using CheckMate.Application.Common.Results;
using CheckMate.Application.DTOs.RoomUnavailability;
using CheckMate.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface IRoomUnavailabilityService
    {
        Task<Result<RoomUnavailabilityDto>> CreateRoomUnavailabilityAsync(RoomUnavailabilityCreationDto dto, User user);

        Task<IEnumerable<RoomUnavailabilityDto>> GetUnavailableRoomsBetweenTwoDates(DateOnly start, DateOnly end);
    }
}
