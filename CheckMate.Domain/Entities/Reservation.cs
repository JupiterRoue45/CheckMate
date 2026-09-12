using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMate.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateOnly ArrivalDate { get; set; }

        [Required]
        public DateOnly DepartureDate { get; set; }

        [Required]
        public int NumberOfAdults { get; set; }

        [Required]
        public int NumberOfChildren { get; set; }

        [Required]
        public int NumberOfInfants { get; set; }

        [Required]
        public int ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }

        [Required]
        public int BookerId { get; set; }
        public Client Booker { get; set; }
    }
}
