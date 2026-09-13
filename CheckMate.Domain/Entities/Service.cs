using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public string ServiceName { get; set; } 

        public string? ServiceDescription { get; set; }

        [Required]
        public decimal ServiceUnitPrice { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
