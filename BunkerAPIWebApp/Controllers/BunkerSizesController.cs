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
    public class BunkerSizesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public BunkerSizesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/BunkerSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BunkerSize>>> GetBunkerSizes()
        {
            return await _context.BunkerSizes.ToListAsync();
        }

        // GET: api/BunkerSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BunkerSize>> GetBunkerSize(int id)
        {
            var bunkerSize = await _context.BunkerSizes.FindAsync(id);

            if (bunkerSize == null)
            {
                return NotFound();
            }

            return bunkerSize;
        }

        // PUT: api/BunkerSizes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBunkerSize(int id, BunkerSize bunkerSize)
        {
            if (id != bunkerSize.Id)
            {
                return BadRequest();
            }

            _context.Entry(bunkerSize).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BunkerSizeExists(id))
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

        // POST: api/BunkerSizes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BunkerSize>> PostBunkerSize(BunkerSize bunkerSize)
        {
            _context.BunkerSizes.Add(bunkerSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBunkerSize", new { id = bunkerSize.Id }, bunkerSize);
        }

        // DELETE: api/BunkerSizes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBunkerSize(int id)
        {
            var bunkerSize = await _context.BunkerSizes.FindAsync(id);
            if (bunkerSize == null)
            {
                return NotFound();
            }

            _context.BunkerSizes.Remove(bunkerSize);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BunkerSizeExists(int id)
        {
            return _context.BunkerSizes.Any(e => e.Id == id);
        }
    }
}
