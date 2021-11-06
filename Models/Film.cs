using System.Collections.Generic;

namespace CinemaApi.Models
{
    public class Film
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }

        public ICollection<Showtime> Showtimes { get; set; }
    }
}