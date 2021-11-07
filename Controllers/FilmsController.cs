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
            return await _context.Films.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(long id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        [HttpPost]
        public async Task<ActionResult<FilmsController>> PostTodoItem(CreateFilmDto createFilmDto)
        {
            Film film = new Film();

            film.Title = createFilmDto.Title;

            film.Runtime = createFilmDto.Runtime;

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return Ok(film);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(long id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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
        public async Task<IActionResult> DeleteFilm(long id)
        {

            // var film = await _context.Films.FindAsync(id);

            var film = _context.Films.Where(b => b.Id == id).Include(b => b.Showtimes).FirstOrDefault();

            if (film == null)
            {
                return NotFound();
            }

            if (film.Showtimes.Count > 0)
            {
                return BadRequest();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(long id)
        {
            return _context.Films.Any(e => e.Id == id);
        }
    }
}
