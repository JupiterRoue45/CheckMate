using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class OngoingInvoice
    {
        public int OngoingInvoiceId { get; set; }

        public int ReservationRoomId { get; set; }
        public ReservationRoom ReservationRoom { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? InvoiceNumber { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
