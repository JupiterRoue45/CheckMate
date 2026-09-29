using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CheckMateDbContext _context;

        public RoomRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.AsNoTracking().ToListAsync();
        }

        public async Task<Room?> Get(int Id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == Id);
        }
    }
}
