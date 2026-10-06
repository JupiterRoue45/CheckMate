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
        public async Task Create(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
        }

        public async Task<RoomType?> Get(int Id)
        {
            return await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.RoomTypeId == Id);
        }

        public async Task<IEnumerable<RoomType>> GetAll()
        {
            return await _context.RoomTypes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> VerifyRoomTypeExistence(int roomTypeId)
        {
            return await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == roomTypeId);
        }

        public async Task<bool> VerifyRoomTypeExistenceFromName(string roomTypeName)
        {
            return await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeName == roomTypeName);
        }
    }
}
