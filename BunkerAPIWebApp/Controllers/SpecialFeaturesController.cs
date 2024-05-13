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
    public class SpecialFeaturesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public SpecialFeaturesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/SpecialFeatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialFeature>>> GetSpecialFeatures()
        {
            return await _context.SpecialFeatures.ToListAsync();
        }

        // GET: api/SpecialFeatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialFeature>> GetSpecialFeature(int id)
        {
            var specialFeature = await _context.SpecialFeatures.FindAsync(id);

            if (specialFeature == null)
            {
                return NotFound();
            }

            return specialFeature;
        }

        // PUT: api/SpecialFeatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialFeature(int id, SpecialFeature specialFeature)
        {
            if (id != specialFeature.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialFeatureExists(id))
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

        // POST: api/SpecialFeatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecialFeature>> PostSpecialFeature(SpecialFeature specialFeature)
        {
            _context.SpecialFeatures.Add(specialFeature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialFeature", new { id = specialFeature.Id }, specialFeature);
        }

        // DELETE: api/SpecialFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialFeature(int id)
        {
            var specialFeature = await _context.SpecialFeatures.FindAsync(id);
            if (specialFeature == null)
            {
                return NotFound();
            }

            _context.SpecialFeatures.Remove(specialFeature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialFeatureExists(int id)
        {
            return _context.SpecialFeatures.Any(e => e.Id == id);
        }
    }
}
