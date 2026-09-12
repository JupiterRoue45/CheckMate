using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class DailyService
    {
        [Required]
        public int ReservationRoomId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
