using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class CreateShowtimeDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public long FilmId { get; set; }

        [Required]
        public long RoomId { get; set; }
    }
}