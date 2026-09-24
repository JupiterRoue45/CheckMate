using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Authentification
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime AccessTokenExpiresAt { get; set; }
    }
}
