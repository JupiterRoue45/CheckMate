using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();

        Task<Room?> GetByIdAsync(int Id);

        Task CreateAsync(Room room);

        Task<bool> VerifyRoomExistenceFromRoomNumberAsync(string number);
    }
}
