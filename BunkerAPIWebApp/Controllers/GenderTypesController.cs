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
    public class GenderTypesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public GenderTypesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/GenderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderType>>> GetGenderTypes()
        {
            return await _context.GenderTypes.ToListAsync();
        }

        // GET: api/GenderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenderType>> GetGenderType(int id)
        {
            var genderType = await _context.GenderTypes.FindAsync(id);

            if (genderType == null)
            {
                return NotFound();
            }

            return genderType;
        }

        // PUT: api/GenderTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenderType(int id, GenderType genderType)
        {
            if (id != genderType.Id)
            {
                return BadRequest();
            }

            _context.Entry(genderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderTypeExists(id))
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

        // POST: api/GenderTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenderType>> PostGenderType(GenderType genderType)
        {
            _context.GenderTypes.Add(genderType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenderType", new { id = genderType.Id }, genderType);
        }

        // DELETE: api/GenderTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenderType(int id)
        {
            var genderType = await _context.GenderTypes.FindAsync(id);
            if (genderType == null)
            {
                return NotFound();
            }

            _context.GenderTypes.Remove(genderType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenderTypeExists(int id)
        {
            return _context.GenderTypes.Any(e => e.Id == id);
        }
    }
}
