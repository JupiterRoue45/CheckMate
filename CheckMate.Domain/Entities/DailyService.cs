using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class DailyService
    {
        public int ReservationRoomId { get; set; }
        public ReservationRoom ReservationRoom { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
