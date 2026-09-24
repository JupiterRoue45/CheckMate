using CheckMate.Infrastructure.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Tuple<string, string>> GenerateAccessTokenAsync(string UserId);
        RefreshToken GenerateRefreshToken(string jwtId, string userId);
    }
}
