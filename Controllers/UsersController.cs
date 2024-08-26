using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPIWebApp.Models;

namespace StoreAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StoreAPIContext _context;

        public UsersController(StoreAPIContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .Select(u => new {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Orders = u.Orders.Count(),
                    Reviews = u.Reviews.Count()
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Customers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

           
            var existingUser = await _context.Customers
                .Where(u => (u.Name == user.Name || u.Email == user.Email) && u.Id != id)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return Conflict(new { message = "A user with the same name or email already exists." });
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          
            var existingUser = await _context.Customers
                .Where(u => u.Name == user.Name || u.Email == user.Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return Conflict(new { message = "A user with the same name or email already exists." });
            }

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Customers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
