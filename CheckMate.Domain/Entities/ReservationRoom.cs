using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class ReservationRoom
    {
        public int ReservationRoomId { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int? RoomId { get; set; }
        public Room? Room { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsBlocked { get; set; }

        public string? Comment { get; set; }
    }
}
