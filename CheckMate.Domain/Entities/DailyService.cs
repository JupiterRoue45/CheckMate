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
        public ReservationRoom ReservationRoom { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
