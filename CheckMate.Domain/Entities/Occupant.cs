using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Occupant
    {
        [Required]
        public int ReservationRoomId { get; set; }
        public ReservationRoom ReservationRoom { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Required]
        public DateOnly StartingDate { get; set; }

        public DateOnly? EndingDate { get; set; }
    }
}
