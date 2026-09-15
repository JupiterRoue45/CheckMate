using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public int? AddressNumber { get; set; }

        public string StreetName { get; set; }

        public string AddressLabel { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
