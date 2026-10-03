using CheckMate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAll();

        Task<Room?> Get(int Id);

        Task Create(Room room);

        Task<bool> VerifyRoomExistenceFromRoomNumber(string number);
    }
}
