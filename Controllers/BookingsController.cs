using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Models;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly MainContext _context;

        public BookingsController(MainContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostTodoItem(CreateBookingDto createBookingDto)
        {
            Booking booking = new Booking();

            var showtime = await _context.Showtimes.FindAsync(createBookingDto.ShowtimeId);
            var user = await _context.Users.FindAsync(createBookingDto.UserId);

            if (user == null || showtime == null)
            {
                return BadRequest();
            }

            var existingBooking = _context.Bookings.Where(b => b.ShowtimeId == createBookingDto.ShowtimeId && b.Seat == createBookingDto.Seat).FirstOrDefault();

            if (existingBooking == null)
            {
                return BadRequest();
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(long id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
