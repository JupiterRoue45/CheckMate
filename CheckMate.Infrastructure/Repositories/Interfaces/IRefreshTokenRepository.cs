using CheckMate.Infrastructure.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task CreateToken(RefreshToken refreshToken);

        Task<RefreshToken?> GetRefreshToken(string token);

        Task Revoke(RefreshToken refreshToken);
    }
}
