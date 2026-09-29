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

        public bool IsAdult { get; set; }

    }
}
