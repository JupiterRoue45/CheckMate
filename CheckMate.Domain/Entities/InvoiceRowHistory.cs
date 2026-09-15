using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class InvoiceRowHistory
    {
        public int InvoiceRowHistoryId { get; set; }

        public int InvoiceRowId { get; set; }
        public InvoiceRow InvoiceRow { get; set; }

        public DateTime CreationDateTime { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int UserId { get; set; }
    }
}
