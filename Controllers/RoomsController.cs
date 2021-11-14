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
    public class RoomsController : ControllerBase
    {
        private readonly MainContext _context;

        public RoomsController(MainContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = rooms });
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Room>> GetRoom(long id)
        // {
        //     var room = await _context.Rooms.FindAsync(id);

        //     if (room == null)
        //     {
        //         return NotFound();
        //     }

        //     return room;
        // }

        [HttpPost]
        public async Task<ActionResult<RoomsController>> PostRoom(CreateRoomDto createRoomDto)
        {
            Room room = new Room();

            room.Number = createRoomDto.Number;

            room.Seats = createRoomDto.Seats;

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new Response { Content = room });
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutRoom(long id, Room room)
        // {
        //     if (id != room.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(room).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!RoomExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteRoom(long id)
        // {

        //     var room = _context.Rooms.Where(b => b.Id == id).Include(b => b.Showtimes).FirstOrDefault();

        //     if (room == null)
        //     {
        //         return NotFound();
        //     }

        //     if (room.Showtimes.Count > 0)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Rooms.Remove(room);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool RoomExists(long id)
        // {
        //     return _context.Rooms.Any(e => e.Id == id);
        // }
    }
}
