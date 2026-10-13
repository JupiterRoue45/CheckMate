using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class RoomUnavailabilityRepository : IRoomUnavailabilityRepository
    {
        private readonly CheckMateDbContext _context;

        public RoomUnavailabilityRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RoomUnavailability roomUnavailability)
        {
            await _context.RoomUnavailabilities.AddAsync(roomUnavailability);
        }

        public async Task<IEnumerable<RoomUnavailability>> GetAllAsync()
        {
            return await _context.RoomUnavailabilities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomUnavailability>> GetBetweenTwoDateAsync(DateOnly start, DateOnly end, bool inclusive = true)
        {
            IQueryable<RoomUnavailability> roomUnavailabilities = _context.RoomUnavailabilities.AsNoTracking();

            if (inclusive)
                roomUnavailabilities = roomUnavailabilities.Where(ru => ru.StartingDate >= start && ru.EndingDate <= end);
            else
                roomUnavailabilities = roomUnavailabilities.Where(ru => ru.StartingDate > start && ru.EndingDate < end);

            return await roomUnavailabilities
                .Include(ru => ru.Room)
                .ToListAsync();

        }

        public async Task<bool> IsRoomUnavailable(int roomId, DateOnly start, DateOnly end)
        {
            return await _context.RoomUnavailabilities
                .Where(ur => ur.RoomId == roomId
                    && ur.StartingDate >= start
                    && ur.EndingDate <= end)
                .AsNoTracking()
                .AnyAsync();
        }
    }
}
