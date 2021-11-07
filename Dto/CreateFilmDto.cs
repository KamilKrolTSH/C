using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class CreateFilmDto
    {
        [Required]
        [StringLength(50)]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [Range(1, 500)]
        public int Runtime { get; set; }
    }
}