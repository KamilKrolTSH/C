using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

    }
}