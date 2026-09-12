using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class ReservationType
    {
        [Key]
        public int ReservationTypeId { get; set; }

        [Required]
        public string ReservationTypeCode { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool FreeCancellation { get; set; }
    }
}
