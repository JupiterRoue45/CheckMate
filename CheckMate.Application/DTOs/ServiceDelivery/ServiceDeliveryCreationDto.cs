using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.ServiceDelivery
{
    public class ServiceDeliveryCreationDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
