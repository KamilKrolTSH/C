namespace CinemaApi.Models
{
    public class Booking
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long ShowtimeId { get; set; }

        public int Seat { get; set; }
    }
}