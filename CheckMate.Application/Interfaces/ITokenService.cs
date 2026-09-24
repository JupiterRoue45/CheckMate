using CheckMate.Application.DTOs.Authentification;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.Interfaces
{
    public interface ITokenService
    {
        Task<AccessTokenGenerationDto> GenerateAccessTokenAsync(User user, IList<string> roles);
        RefreshToken GenerateRefreshToken(string jwtId, string userId);
        Task<bool> RevokeRefreshToken(string refreshToken);
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task<AuthResponseDto?> RefreshToken(string token);
    }
}
