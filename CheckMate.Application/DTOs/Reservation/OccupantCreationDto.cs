using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Reservation
{
    public class OccupantCreationDto
    {
        public int PersonId { get; set; }
        public DateOnly? Start { get; set; }
        public DateOnly? End { get; set; }
    }
}
