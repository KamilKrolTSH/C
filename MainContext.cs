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
        public DbSet<User> UserItems { get; set; }
    }
}