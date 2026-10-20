using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Reservation
{
    public class ReservationRoomCreationDto
    {
        public int RoomTypeId { get; set; }
        public int? RoomId { get; set; }
        public string? Comment { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public int CancellationPolicyId { get; set; }
        public IEnumerable<OccupantCreationDto> Occupants { get; set; }
        public IEnumerable<DailyServiceCreationDto> Services { get; set; }
    }
}
