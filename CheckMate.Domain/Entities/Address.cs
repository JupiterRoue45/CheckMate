using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        public int? AddresNumber { get; set; }

        [Required]
        public string AddressLabel { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
