using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.Models
{
    public class UpdateFilmDto
    {
        [Required]
        public long Id { get; set; }

        [StringLength(50)]
        [MinLength(1)]
        public string Title { get; set; }

        [Range(1, 500)]
        public int Runtime { get; set; }
    }
}