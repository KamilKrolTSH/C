namespace CinemaApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserDto
    {
        [Required]
        [StringLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}