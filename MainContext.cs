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


            builder.Entity<Film>().HasData(new Film { Id = 1, Title = "a", Runtime = 60 });
            builder.Entity<Film>().HasData(new Film { Id = 2, Title = "b", Runtime = 120 });

            builder.Entity<Room>().HasData(new Room { Id = 1, Number = 1, Seets = 10 });
            builder.Entity<Room>().HasData(new Room { Id = 2, Number = 2, Seets = 20 });

            builder.Entity<Showtime>().HasData(new Showtime { Id = 1, Date = System.DateTime.Now.AddHours(2), FilmId = 1, RoomId = 1 });
            builder.Entity<Showtime>().HasData(new Showtime { Id = 2, Date = System.DateTime.Now.AddHours(4), FilmId = 2, RoomId = 2 });
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Showtime> Showtimes { get; set; }

    }
}