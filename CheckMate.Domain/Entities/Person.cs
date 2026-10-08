using CheckMate.Domain.Enums;
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

        public bool IsAdult { get; set; }

        public GenderEnum Gender { get; set; }

    }
}
