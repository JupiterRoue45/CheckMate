using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Person : Client
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? PhoneAreaCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
