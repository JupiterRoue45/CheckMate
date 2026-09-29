using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> Get(int Id);
        Task Create(RoomType roomType);
    }
}
