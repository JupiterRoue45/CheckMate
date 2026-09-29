using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly CheckMateDbContext _context;

        public RoomTypeRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public Task Create(RoomType roomType)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomType?> Get(int Id)
        {
            return await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.RoomTypeId == Id);
        }
    }
}
