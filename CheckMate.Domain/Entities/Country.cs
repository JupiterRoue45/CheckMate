using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string PhoneDialCode { get; set; }
    }
}
