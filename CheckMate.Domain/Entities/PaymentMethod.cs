using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodCode { get; set; }

        public string PaymentMethodDescription { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
    }
}
