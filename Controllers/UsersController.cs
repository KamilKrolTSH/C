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
    public class UsersController : ControllerBase
    {
        private readonly MainContext _context;
        private Crypto _crypto;

        public UsersController(MainContext context)
        {

            _context = context;
            _crypto = new Crypto();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostTodoItem(CreateUserDto user)
        {
            User newUser = new User();
            user.Name = user.Name;
            user.Password = _crypto.cezarCode(user.Password);

            _context.UserItems.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok();
            // return CreatedAtAction("GetUser", new { id = newUser.Id }, user);
        }
    }
}
