using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class ComplementaryServiceRepository : IComplementaryServiceRepository
    {
        private readonly CheckMateDbContext _context;

        public ComplementaryServiceRepository(CheckMateDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(ComplementaryService complementaryService)
        {
            await _context.ComplementaryServices.AddAsync(complementaryService);
        }

        public async Task<IEnumerable<ComplementaryService>> GetAllAsync()
        {
            return await _context.ComplementaryServices
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ComplementaryService>> GetAllComplementaryServicesOfAReservationRoom(int reservationRoomId)
        {
            return await _context.ComplementaryServices
                .Where(cs => cs.ReservationRoomId == reservationRoomId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
