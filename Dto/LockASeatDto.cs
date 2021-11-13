using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class LockASeatDto
    {
        [Required]
        public long ShowtimeId { get; set; }

        [Required]
        public int Seat { get; set; }
    }
}