using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    /// <summary>
    /// Represents the type of a room.
    /// </summary>
    public class RoomType
    {
        public int RoomTypeId { get; set; }

        public string RoomTypeName { get; set; }

        public string? Description { get; set; }

        public int Rank { get; set; }

        public int MaxOccupancy { get; set; }
    }
}
