using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class InvoiceRowHistory
    {
        [Key]
        public int InvoiceRowHistoryId { get; set; }

        [Required]
        public int InvoiceRowId { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        [Required]
        public decimal NewPrice { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
