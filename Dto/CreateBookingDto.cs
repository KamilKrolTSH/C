using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class CreateBookingDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long ShowtimeId { get; set; }

        [Required]
        [Range(1, 50)]
        public int Seat { get; set; }
    }
}