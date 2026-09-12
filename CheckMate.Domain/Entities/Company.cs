using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Company : Client
    {
        [Required]
        public string CompanyName { get; set; }
        public string? TVA { get; set; }
    }
}
