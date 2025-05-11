using api.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class UserController : Controller
    {
        private readonly AppAzContext _context;
        public UserController(AppAzContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/user")]
        public async Task<IActionResult> Get()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        [HttpDelete]
        [Route("api/user/{newId}")]
        public async Task<IActionResult> Delete(Guid newId)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(newId);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost]
        [Route("api/user")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPut]
        [Route("api/user/{newId}")]
        public async Task<IActionResult> Put(Guid newId, [FromBody] PositionDTO pos)
        {
            var user = await _context.Users.FindAsync(newId);
            if (user == null)
            {
                return NotFound();
            }
            user.PositionX = pos.posX;
            user.PositionY = pos.posY;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}