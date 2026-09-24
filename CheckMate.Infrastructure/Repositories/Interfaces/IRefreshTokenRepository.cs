using CheckMate.Infrastructure.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void CreateToken(RefreshToken refreshToken);

        Task<RefreshToken?> GetRefreshToken(string token);

        void Revoke(RefreshToken refreshToken);
    }
}
