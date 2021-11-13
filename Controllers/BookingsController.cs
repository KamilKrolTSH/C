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
                if (existingBooking.Confirmed == true)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SEAT_ALREADY_BOOKED" });
                }

                if (DateTime.Compare(existingBooking.dateToConfirm, DateTime.Now) > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SEAT_IS_LOCKED" });
                }
                booking = existingBooking;
            }

            booking.dateToConfirm = DateTime.Now.Add(new TimeSpan(0, 1, 3));
            booking.UserName = userName;
            booking.Confirmed = false;
            booking.Seat = lockASeatDto.Seat;
            booking.ShowtimeId = showtime.Id;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { });
        }

        [Route("confirm")]
        [HttpPost]
        public async Task<ActionResult> ConfirmBooking(LockASeatDto lockASeatDto)
        {
            string userName = HttpContext.User.Identity.Name;

            var showtime = await _context.Showtimes.FindAsync(lockASeatDto.ShowtimeId);

            if (showtime == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SHOWTIME_DOES_NOT_EXISTS" });
            }

            var booking = await _context.Bookings.Where((b) => b.ShowtimeId == showtime.Id && b.Seat == lockASeatDto.Seat).FirstOrDefaultAsync();

            if (booking == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "FIRST_LOCK_RECORD" });
            }

            if (booking.Confirmed == true)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SEAT_ALREADY_CONFIRMED" });
            }

            if (booking.UserName != userName)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "CAN_NOT_CONFIRM_SOMEONE_ELSE_LOCKED_SEAT" });
            }

            if (DateTime.Compare(booking.dateToConfirm, DateTime.Now) < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "FIRST_LOCK_RECORD" });
            }

            booking.Confirmed = true;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { });
        }
    }
}
