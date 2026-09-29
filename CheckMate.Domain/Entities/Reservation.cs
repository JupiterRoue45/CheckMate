using CheckMate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public DateOnly ArrivalDate { get; set; }

        public DateOnly DepartureDate { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public int ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }

        public string UserId { get; set; }

        public int BookerId { get; set; }
        public Client Booker { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
