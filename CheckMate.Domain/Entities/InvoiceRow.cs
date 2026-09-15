using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class InvoiceRow
    {
        public int InvoiceRowId { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public int Quantity { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public int OngoingInvoiceId { get; set; }

        public OngoingInvoice OngoingInvoice { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
