using CheckMate.Infrastructure.Configurations;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Identity.Tokens;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CheckMate.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings jwtSettings;
        public TokenService(
            IConfiguration configuration,
            UserManager<User> userManager,
            IOptions<JwtSettings> options)
        {
            _configuration = configuration;
            _userManager = userManager;
            jwtSettings = options.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="roles"></param>
        /// <param name="jwtId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Tuple<string, string>> GenerateAccessTokenAsync(string UserId)
        {
            // We verify if the user exists
            User user = await _userManager.FindByIdAsync(UserId) ??
                throw new Exception($"Unexistant user with the userId : {UserId}");

            // We retrieve his roles
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

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
                new Claim(JwtRegisteredClaimNames.Sub, UserId),

                new Claim(JwtRegisteredClaimNames.Jti, jwtId),

                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}")

            };

            claims.AddRange(
                roles.Select(role => 
                new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Mandatory claims in the payload part
            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Tuple.Create(tokenHandler.CreateToken(token), jwtId);
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
    }
}
