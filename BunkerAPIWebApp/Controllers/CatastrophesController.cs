using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BunkerAPIWebApp.Models;

namespace BunkerAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatastrophesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public CatastrophesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Catastrophes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catastrophe>>> GetCatastrophes()
        {
            return await _context.Catastrophes.ToListAsync();
        }

        // GET: api/Catastrophes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catastrophe>> GetCatastrophe(int id)
        {
            var catastrophe = await _context.Catastrophes.FindAsync(id);

            if (catastrophe == null)
            {
                return NotFound();
            }

            return catastrophe;
        }

        // PUT: api/Catastrophes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatastrophe(int id, Catastrophe catastrophe)
        {
            if (id != catastrophe.Id)
            {
                return BadRequest();
            }

            _context.Entry(catastrophe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatastropheExists(id))
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

        // POST: api/Catastrophes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catastrophe>> PostCatastrophe(Catastrophe catastrophe)
        {
            _context.Catastrophes.Add(catastrophe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatastrophe", new { id = catastrophe.Id }, catastrophe);
        }

        // DELETE: api/Catastrophes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatastrophe(int id)
        {
            var catastrophe = await _context.Catastrophes.FindAsync(id);
            if (catastrophe == null)
            {
                return NotFound();
            }

            _context.Catastrophes.Remove(catastrophe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatastropheExists(int id)
        {
            return _context.Catastrophes.Any(e => e.Id == id);
        }
    }
}
