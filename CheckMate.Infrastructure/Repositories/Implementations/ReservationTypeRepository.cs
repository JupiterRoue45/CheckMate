using CheckMate.Domain.Entities;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class ReservationTypeRepository : IReservationTypeRepository
    {
        private readonly CheckMateDbContext _context;

        public ReservationTypeRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ReservationType type)
        {
            await _context.ReservationTypes.AddAsync(type);
        }

        public async Task<IEnumerable<ReservationType>> GetAllAsync()
        {
            return await _context.ReservationTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ReservationType>> GetByCodeAsync(string code)
        {
            return await _context.ReservationTypes
                .AsNoTracking()
                .Where(rt => rt.ReservationTypeCode.Contains(code))
                .ToListAsync();
        }

        public async Task<ReservationType?> GetByIdAsync(int id)
        {
            return await _context.ReservationTypes.FindAsync(id);
        }

        public async Task<bool> VerifyCodeExistance(string code)
        {
            return await _context.ReservationTypes.AnyAsync(rt => rt.ReservationTypeCode.Equals(code));
        }
    }
}
