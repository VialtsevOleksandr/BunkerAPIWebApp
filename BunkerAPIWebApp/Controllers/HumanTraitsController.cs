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
    public class HumanTraitsController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public HumanTraitsController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/HumanTraits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HumanTrait>>> GetHumanTraits()
        {
            return await _context.HumanTraits.ToListAsync();
        }

        // GET: api/HumanTraits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HumanTrait>> GetHumanTrait(int id)
        {
            var humanTrait = await _context.HumanTraits.FindAsync(id);

            if (humanTrait == null)
            {
                return NotFound();
            }

            return humanTrait;
        }

        // PUT: api/HumanTraits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumanTrait(int id, HumanTrait humanTrait)
        {
            if (id != humanTrait.Id)
            {
                return BadRequest();
            }

            _context.Entry(humanTrait).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanTraitExists(id))
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

        // POST: api/HumanTraits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HumanTrait>> PostHumanTrait(HumanTrait humanTrait)
        {
            _context.HumanTraits.Add(humanTrait);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHumanTrait", new { id = humanTrait.Id }, humanTrait);
        }

        // DELETE: api/HumanTraits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHumanTrait(int id)
        {
            var humanTrait = await _context.HumanTraits.FindAsync(id);
            if (humanTrait == null)
            {
                return NotFound();
            }

            _context.HumanTraits.Remove(humanTrait);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanTraitExists(int id)
        {
            return _context.HumanTraits.Any(e => e.Id == id);
        }
    }
}
