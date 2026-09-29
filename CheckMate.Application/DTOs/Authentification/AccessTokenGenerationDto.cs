using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Authentification
{
    public class AccessTokenGenerationDto
    {
        public string AccessToken { get; set; }
        public string JwtId { get; set; }
        public int AccessTokenExpiryMinutes { get; set; }
    }
}
