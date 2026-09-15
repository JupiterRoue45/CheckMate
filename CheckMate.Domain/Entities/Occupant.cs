using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Occupant
    {
        public int ReservationRoomId { get; set; }
        public ReservationRoom ReservationRoom { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public DateOnly StartingDate { get; set; }

        public DateOnly? EndingDate { get; set; }
    }
}
