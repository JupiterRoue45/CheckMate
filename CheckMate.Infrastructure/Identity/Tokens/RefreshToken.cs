using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Identity.Tokens
{
    public class RefreshToken
    {
        public int Id { get; set; }
        
        // The refresh token string
        public string Token { get; set; }

        // Helps invalidate refresh token when the associated token is revoked or suspected compromised
        public string JwtId { get; set; }

        // Token expiration datetime
        public DateTime Expires { get; set; }

        // Indicates if the token has been revoked
        public bool IsRevoked { get; set; }

        // Date when token was revoked
        public DateTime? RevokedAt { get; set; }

        // Time when token was created
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
