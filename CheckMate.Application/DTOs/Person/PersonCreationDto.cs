using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Person
{
    public class PersonCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsAdult { get; set; }
        public string? PhoneAreaCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
