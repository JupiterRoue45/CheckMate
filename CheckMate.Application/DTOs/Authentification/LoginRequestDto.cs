using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.Authentification
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Please insert a valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "No empty passwords")]
        public string Password { get; set; }
    }
}
