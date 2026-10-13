using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRoomUnavailabilityRepository 
    {
        Task<IEnumerable<RoomUnavailability>> GetBetweenTwoDateAsync(DateOnly start, DateOnly end, bool inclusive = true);
        Task<IEnumerable<RoomUnavailability>> GetAllAsync();
        Task<bool> IsRoomUnavailable(int roomId, DateOnly start, DateOnly end);
        Task CreateAsync(RoomUnavailability roomUnavailability);
    }
}
