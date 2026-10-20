using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Reservation
{
    public class DailyServiceCreationDto
    {
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
