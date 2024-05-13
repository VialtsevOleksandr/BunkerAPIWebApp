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
    public class PhobiasController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public PhobiasController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Phobias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phobia>>> GetPhobias()
        {
            return await _context.Phobias.ToListAsync();
        }

        // GET: api/Phobias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phobia>> GetPhobia(int id)
        {
            var phobia = await _context.Phobias.FindAsync(id);

            if (phobia == null)
            {
                return NotFound();
            }

            return phobia;
        }

        // PUT: api/Phobias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhobia(int id, Phobia phobia)
        {
            if (id != phobia.Id)
            {
                return BadRequest();
            }

            _context.Entry(phobia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhobiaExists(id))
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

        // POST: api/Phobias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Phobia>> PostPhobia(Phobia phobia)
        {
            _context.Phobias.Add(phobia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhobia", new { id = phobia.Id }, phobia);
        }

        // DELETE: api/Phobias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhobia(int id)
        {
            var phobia = await _context.Phobias.FindAsync(id);
            if (phobia == null)
            {
                return NotFound();
            }

            _context.Phobias.Remove(phobia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhobiaExists(int id)
        {
            return _context.Phobias.Any(e => e.Id == id);
        }
    }
}
