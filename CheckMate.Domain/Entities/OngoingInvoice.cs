using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class OngoingInvoice
    {
        public int OngoingInvoiceId { get; set; }
        public int ReservationRoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InvoiceNumber { get; set; }
    }
}
