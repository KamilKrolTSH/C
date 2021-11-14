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
    public class FilmsController : ControllerBase
    {
        private readonly MainContext _context;

        public FilmsController(MainContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            var films = await _context.Films.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = films });

        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Film>> GetFilm(long id)
        // {
        //     var film = await _context.Films.FindAsync(id);

        //     if (film == null)
        //     {
        //         return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "FILM_DOES_NOT_EXIST" });
        //     }

        //     var showtimes = await _context.Showtimes.Where((b) => b.FilmId == film.Id && b.Date > DateTime.Now).ToListAsync();
        //     film.Showtimes = showtimes;

        //     return StatusCode(StatusCodes.Status200OK, new Response { Content = film });
        // }

        [HttpPost]
        public async Task<ActionResult<FilmsController>> PostFilm(CreateFilmDto createFilmDto)
        {
            Film film = new Film();

            film.Title = createFilmDto.Title;

            film.Runtime = createFilmDto.Runtime;

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = film });
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFilm(long id, UpdateFilmDto film)
        // {
        //     if (id != film.Id)
        //     {
        //         return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "ID_CAN_NOT_BE_DIFFERENT_THAN_FILM_ID" });
        //     }

        //     _context.Entry(film).State = EntityState.Modified;

        //     var foundFilm = await _context.Films.FindAsync(id);

        //     if (foundFilm == null)
        //     {
        //         return NotFound();
        //     }

        //     var showtimes = await _context.Showtimes.Where((b) => b.FilmId == film.Id && b.Date > DateTime.Now).ToListAsync();

        //     if (showtimes.Count > 0)
        //     {
        //         return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "SHOWTIME_EXISTS" });
        //     }

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!FilmExists(id))
        //         {
        //             return StatusCode(StatusCodes.Status400BadRequest, new Response { Error = "FILM_DOES_NOT_EXIST" });
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return StatusCode(StatusCodes.Status200OK, new Response { });
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteFilm(long id)
        // {
        //     var film = _context.Films.Where(b => b.Id == id).Include(b => b.Showtimes).FirstOrDefault();

        //     if (film == null)
        //     {
        //         return NotFound();
        //     }

        //     if (film.Showtimes.Count > 0)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Films.Remove(film);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool FilmExists(long id)
        // {
        //     return _context.Films.Any(e => e.Id == id);
        // }
    }
}
