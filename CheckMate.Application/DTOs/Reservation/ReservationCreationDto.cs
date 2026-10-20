using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Application.DTOs.Reservation
{
    public class ReservationCreationDto
    {
        public DateOnly? Arrival { get; set; }
        public DateOnly? Departure { get; set; }
        public int ReservationType { get; set; }
        public int BookerId { get; set; }
        public string Number { get; set; }
        public string? Voucher { get; set; }
        public IEnumerable<ReservationRoomCreationDto> rooms { get; set; }
    }
}
