using System;
using System.Collections.Generic;

namespace CinemaApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}