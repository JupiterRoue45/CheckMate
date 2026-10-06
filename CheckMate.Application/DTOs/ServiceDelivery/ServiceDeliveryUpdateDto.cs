using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.ServiceDelivery
{
    public class ServiceDeliveryUpdateDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
