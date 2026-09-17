using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class RoomUnavailability
    {
        public int RoomUnavailabilityId { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateOnly StartingDate { get; set; }

        public DateOnly EndingDate { get; set; }

        public string Reason { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
