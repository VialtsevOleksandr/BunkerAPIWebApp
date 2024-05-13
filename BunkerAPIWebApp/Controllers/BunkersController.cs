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
    public class BunkersController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public BunkersController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Bunkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bunker>>> GetBunkers()
        {
            return await _context.Bunkers.ToListAsync();
        }

        // GET: api/Bunkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bunker>> GetBunker(int id)
        {
            var bunker = await _context.Bunkers.FindAsync(id);

            if (bunker == null)
            {
                return NotFound();
            }

            return bunker;
        }

        // PUT: api/Bunkers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBunker(int id, Bunker bunker)
        {
            if (id != bunker.Id)
            {
                return BadRequest();
            }

            _context.Entry(bunker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BunkerExists(id))
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

        // POST: api/Bunkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bunker>> PostBunker(Bunker bunker)
        {
            _context.Bunkers.Add(bunker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBunker", new { id = bunker.Id }, bunker);
        }

        // DELETE: api/Bunkers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBunker(int id)
        {
            var bunker = await _context.Bunkers.FindAsync(id);
            if (bunker == null)
            {
                return NotFound();
            }

            _context.Bunkers.Remove(bunker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BunkerExists(int id)
        {
            return _context.Bunkers.Any(e => e.Id == id);
        }
    }
}
