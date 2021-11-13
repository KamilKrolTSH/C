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
        public async Task<ActionResult> LockASeat(LockASeatDto lockASeatDto)
        {
            var showtime = await _context.Showtimes.FindAsync(lockASeatDto.ShowtimeId);

            if (showtime == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SHOWTIME_DOES_NOT_EXISTS" });
            }

            string userName = HttpContext.User.Identity.Name;

            var existingBooking = await _context.Bookings.Where((b) => b.ShowtimeId == showtime.Id && b.Seat == lockASeatDto.Seat).FirstOrDefaultAsync();

            Booking booking = new Booking();

            if (existingBooking != null)
            {
                if (existingBooking.Confirmed == true && showtime.Date <= DateTime.Now)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SEAT_ALREADY_BOOKED" });
                }

                if (existingBooking.dateToConfirm <= DateTime.Now)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Error = "SEAT_IS_LOCKED" });
                }
                booking = existingBooking;
            }

            booking.dateToConfirm = DateTime.Now.Add(new TimeSpan(0, 1, 5));
            booking.UserName = userName;
            booking.Confirmed = false;
            booking.Seat = lockASeatDto.Seat;
            booking.ShowtimeId = showtime.Id;

            var x = _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { });

        }


        [HttpPost]
        public async Task<ActionResult<Booking>> PostTodoItem(CreateBookingDto createBookingDto)
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
