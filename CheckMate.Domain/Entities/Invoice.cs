using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string UserId { get; set; }

        public ICollection<OngoingInvoice> OngoingInvoices { get; set; } = [];
    }
}
