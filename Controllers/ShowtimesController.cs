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
    public class ShowtimesController : ControllerBase
    {
        private readonly MainContext _context;

        public ShowtimesController(MainContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Showtime>>> GetShowtimes()
        {
            return await _context.Showtimes.Include((b) => b.Film).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Showtime>> GetShowtime(long id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);

            if (showtime == null)
            {
                return NotFound();
            }

            return showtime;
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
