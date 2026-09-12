using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class InvoiceRow
    {
        [Key]
        public int InvoiceRowId { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public Service Service { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Required]
        public int OngoingInvoiceId { get; set; }

        public OngoingInvoice OngoingInvoice { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
