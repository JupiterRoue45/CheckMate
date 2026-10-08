using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Address
{
    public class AddressCreationDto
    {
        public string Label { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? CountryId { get; set; }
    }
}
