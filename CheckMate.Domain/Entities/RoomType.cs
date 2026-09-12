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
        [Key]
        public int RoomTypeId { get; set; }
        [Required]
        public string RoomTypeName { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Rank { get; set; }
        [Required]
        public int MaxOccupancy { get; set; }
    }
}
