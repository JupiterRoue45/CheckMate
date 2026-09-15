using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; } 

        public string? ServiceDescription { get; set; }

        public decimal ServiceUnitPrice { get; set; }

        public bool IsAvailable { get; set; }
    }
}
