using System;

namespace CinemaApi.Models
{
    public class Booking
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public long ShowtimeId { get; set; }

        public int Seat { get; set; }

        public bool Confirmed { get; set; }

        public DateTime dateToConfirm { get; set; }
    }
}