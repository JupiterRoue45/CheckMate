using CheckMate.Infrastructure.Identity.Tokens;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly CheckMateDbContext _context;

        public RefreshTokenRepository(CheckMateDbContext context)
        {
            _context = context;
        }

        public void CreateToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }

        public void Revoke(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
        }
    }
}
