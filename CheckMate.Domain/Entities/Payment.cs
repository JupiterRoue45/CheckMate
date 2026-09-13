using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int OngoingInvoiceId { get; set; }
        public OngoingInvoice OngoingInvoice { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
