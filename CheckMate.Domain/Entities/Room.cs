using CheckMate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        public int? Floor { get; set; }

        public Decimal? Area { get; set; }

        public RoomStatusEnum RoomStatus { get; set; }
    }
}
