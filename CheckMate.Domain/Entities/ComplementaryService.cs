using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class ComplementaryService
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int ReservationRoomId { get; set; }
        public ReservationRoom ReservationRoom { get; set; }

        public DateOnly StartingDate { get; set; }

        public DateOnly EndingDate { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
    }
}
