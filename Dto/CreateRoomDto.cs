using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class CreateRoomDto
    {
        [Required]
        [Range(0, 1000)]
        public int Number { get; set; }

        [Required]
        [Range(1, 50)]
        public int Seats { get; set; }
    }
}