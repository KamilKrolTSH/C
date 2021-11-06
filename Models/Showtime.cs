using System;
using System.Collections.Generic;

namespace CinemaApi.Models
{
    public class Showtime
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public long FilmId { get; set; }

        public long RoomId { get; set; }
    }
}