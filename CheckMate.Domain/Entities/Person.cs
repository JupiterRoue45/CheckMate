using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Person : Client
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateOnly? BirthDate { get; set; }

        public string? PhoneAreaCode { get; set; }

        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
