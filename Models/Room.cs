using System.Collections.Generic;

namespace CinemaApi.Models
{
    public class Room
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public int Seets { get; set; }

        public ICollection<Showtime> Showtimes { get; set; }
    }
}