using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CinemaApi.Models
{
    public class MainContext : IdentityDbContext<ApplicationUser>
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

    }
}