using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Application.DTOs.Room
{
    public class RoomCreationDto
    {
        public string Number { get; set; }

        public int RoomTypeId { get; set; }

        public int? Floor { get; set; }

        public decimal? Area { get; set; }
    }
}
