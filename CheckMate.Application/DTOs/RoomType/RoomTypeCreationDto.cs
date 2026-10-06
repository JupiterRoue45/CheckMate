using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.RoomType
{
    public class RoomTypeCreationDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Rank { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
