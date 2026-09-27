using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.Authentification
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [Length(minimumLength: 10,maximumLength:  256, ErrorMessage = "The username must contain between 10 and 256 chacarters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Length(1, 100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Length(1, 100)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "An email address is required")]
        [MaxLength(50)]
        public string  Email{ get; set; }

        [Required(ErrorMessage = "The password field is mandatory")]
        [MinLength(8, ErrorMessage = "The password field should contain atleast 8 characters")]
        public string Password { get; set; }
    }
}
