using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class ReservationType
    {
        public int ReservationTypeId { get; set; }

        public string ReservationTypeCode { get; set; }

        public string? Description { get; set; }

        public bool FreeCancellation { get; set; }
    }
}
