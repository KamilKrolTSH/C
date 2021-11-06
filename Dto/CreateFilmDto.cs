using System.Collections.Generic;

namespace CinemaApi.Models
{
    public class CreateFilmDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }

    }
}