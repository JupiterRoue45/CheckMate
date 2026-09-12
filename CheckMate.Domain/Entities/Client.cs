using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        public DateTime CreationAt { get; set; }

        [Required]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
