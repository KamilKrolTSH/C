using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace CinemaApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimesController : ControllerBase
    {
        private readonly MainContext _context;

        public ShowtimesController(MainContext context)
        {

            _context = context;
        }

        private DateTime getFilterDate()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan minusTenMinutes = new TimeSpan(0, -10, 0);
            return currentDate.Add(minusTenMinutes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Showtime>>> GetShowtimes()
        {
            var showtimes = await _context.Showtimes.Include((b) => b.Film).Include((b) => b.Room).ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = showtimes });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Showtime>> GetShowtime(long id)
        {

            var showtime = await _context.Showtimes.Include((b) => b.Film).Include((b) => b.Room).FirstOrDefaultAsync((b) => b.Id == id);

            if (showtime == null)
            {
                return NotFound();
            }


            var bookings = await _context.Bookings.Where((b) => b.ShowtimeId == showtime.Id).ToListAsync();

            showtime.Bookings = bookings;

            return StatusCode(StatusCodes.Status200OK, new Response { Content = showtime });
        }

        [HttpPost]
        public async Task<ActionResult<Showtime>> PostTodoItem(CreateShowtimeDto createShowtimeDto)
        {
            Showtime showtime = new Showtime();

            showtime.Date = createShowtimeDto.Date;

            var film = await _context.Films.FindAsync(showtime.FilmId);

            if (film == null)
            {
                return BadRequest();
            }

            var room = await _context.Rooms.FindAsync(showtime.RoomId);

            if (room == null)
            {
                return BadRequest();
            }

            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();

            return Ok(showtime);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowtime(long id, Showtime showtime)
        {
            if (id != showtime.Id)
            {
                return BadRequest();
            }

            _context.Entry(showtime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowtimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowtime(long id)
        {

            var showtime = _context.Showtimes.Where(b => b.Id == id).Include(b => b.Bookings).FirstOrDefault();

            if (showtime == null)
            {
                return NotFound();
            }

            if (showtime.Bookings.Count > 0)
            {
                return BadRequest();
            }

            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowtimeExists(long id)
        {
            return _context.Showtimes.Any(e => e.Id == id);
        }
    }
}
