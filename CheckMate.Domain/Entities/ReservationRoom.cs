using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class ReservationRoom
    {
        [Key]
        public int ReservationRoomId { get; set; }

        [Required]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }

        [Required]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        [Required]
        public bool IsOccupied { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        public string? Comment { get; set; }
    }
}
