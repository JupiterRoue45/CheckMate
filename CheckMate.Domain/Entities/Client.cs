using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        public DateTime CreationAt { get; set; }

        public int? AddressId { get; set; }

        public Address? Address { get; set; }

        public string? PhoneAreaCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
