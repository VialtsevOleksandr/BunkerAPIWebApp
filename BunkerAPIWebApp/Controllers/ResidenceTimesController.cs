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
    public class ResidenceTimesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public ResidenceTimesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/ResidenceTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidenceTime>>> GetResidenceTimes()
        {
            return await _context.ResidenceTimes.ToListAsync();
        }

        // GET: api/ResidenceTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidenceTime>> GetResidenceTime(int id)
        {
            var residenceTime = await _context.ResidenceTimes.FindAsync(id);

            if (residenceTime == null)
            {
                return NotFound();
            }

            return residenceTime;
        }

        // PUT: api/ResidenceTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResidenceTime(int id, ResidenceTime residenceTime)
        {
            if (id != residenceTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(residenceTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenceTimeExists(id))
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

        // POST: api/ResidenceTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResidenceTime>> PostResidenceTime(ResidenceTime residenceTime)
        {
            _context.ResidenceTimes.Add(residenceTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResidenceTime", new { id = residenceTime.Id }, residenceTime);
        }

        // DELETE: api/ResidenceTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidenceTime(int id)
        {
            var residenceTime = await _context.ResidenceTimes.FindAsync(id);
            if (residenceTime == null)
            {
                return NotFound();
            }

            _context.ResidenceTimes.Remove(residenceTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidenceTimeExists(int id)
        {
            return _context.ResidenceTimes.Any(e => e.Id == id);
        }
    }
}
