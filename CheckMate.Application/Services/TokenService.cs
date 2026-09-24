using CheckMate.Infrastructure.Configurations;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CheckMate.Application.DTOs.Authentification;
using CheckMate.Infrastructure.Repositories.Interfaces;
using CheckMate.Application.Interfaces;

namespace CheckMate.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public TokenService(
            IConfiguration configuration,
            UserManager<User> userManager,
            IOptions<JwtSettings> options,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            jwtSettings = options.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="roles"></param>
        /// <param name="jwtId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<AccessTokenGenerationDto> GenerateAccessTokenAsync(User user, IList<string> roles)
        {
            // new JWT security token handler
            var tokenHandler = new JsonWebTokenHandler();

            // Saved in the secrets
            string keyCoded = jwtSettings.SigningKey ??
                throw new Exception("Could not find the Jwt signing key.");

            // Convert it into bytes
            byte[] keyBytes = Convert.FromBase64String(keyCoded);

            // Create our symetric key
            var key = new SymmetricSecurityKey(keyBytes);

            // Every access token will have a unique GUID
            string jwtId = Guid.NewGuid().ToString();

            string issuer = jwtSettings.Issuer;

            string audience = jwtSettings.Audience;

            int accessTokenExpirationMinutes = jwtSettings.AccessTokenExpirationMinutes;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),

                new Claim(JwtRegisteredClaimNames.Jti, jwtId),

                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}")

            };

            claims.AddRange(
                roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Mandatory claims in the payload part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessTokenGenerationDto
            {
                AccessToken = token,
                JwtId = jwtId,
                AccessTokenExpiryMinutes = accessTokenExpirationMinutes
            };
        }

        public RefreshToken GenerateRefreshToken(string jwtId, string userId)
        {
            var randomBytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var refreshTokenExpirationDays = jwtSettings.RefreshTokenExpirationDays;

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),

                JwtId = jwtId,

                Expires = DateTime.UtcNow.AddDays(refreshTokenExpirationDays),

                UserId = userId,

                IsRevoked = false,

                RevokedAt = null
            };
        }

        public async Task<bool> RevokeRefreshToken(string token)
        {
            RefreshToken? refreshToken = await _refreshTokenRepository.GetRefreshToken(token);

            if (refreshToken is null || refreshToken.IsRevoked)
                return false;

            await _refreshTokenRepository.Revoke(refreshToken);

            return true;
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.CreateToken(refreshToken);
        }
    }
}
