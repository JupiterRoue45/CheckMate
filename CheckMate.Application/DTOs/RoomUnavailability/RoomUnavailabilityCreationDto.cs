using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.RoomUnavailability
{
    public class RoomUnavailabilityCreationDto
    {
        public string RoomNumber { get; set; }

        public DateOnly StartingDate { get; set; }

        public DateOnly EndingDate { get; set; }

        public string Reason { get; set; }

        public string? Comment { get; set; }

    }
}
