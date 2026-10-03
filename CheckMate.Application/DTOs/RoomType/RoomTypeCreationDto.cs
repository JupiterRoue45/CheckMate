using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.RoomType
{
    public class RoomTypeCreationDto
    {
        [Required(ErrorMessage ="The room Type name is required")]
        [MaxLength(50, ErrorMessage = "The room type name should not exceed 50 characters")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a positive rank")]
        public int Rank { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of people that a room type can contain, can not be below 1")]
        public int MaxOccupancy { get; set; }
    }
}
