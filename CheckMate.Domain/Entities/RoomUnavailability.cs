using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class RoomUnavailability
    {
        [Key]
        public int RoomUnavailabilityId { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

        [Required]
        public string Reason { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
