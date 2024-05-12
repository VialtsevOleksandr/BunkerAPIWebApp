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
    public class AdditionalInformationsController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public AdditionalInformationsController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/AdditionalInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionalInformation>>> GetAdditionalInformations()
        {
            return await _context.AdditionalInformations.ToListAsync();
        }

        // GET: api/AdditionalInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalInformation>> GetAdditionalInformation(int id)
        {
            var additionalInformation = await _context.AdditionalInformations.FindAsync(id);

            if (additionalInformation == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено додаткової інформації з таким ID." });
            }

            return additionalInformation;
        }

        // PUT: api/AdditionalInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdditionalInformation(int id, AdditionalInformation additionalInformation)
        {
            if (id != additionalInformation.Id)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: ID не відповідає ID додаткової інформації." });
            }

            if (additionalInformation == null || string.IsNullOrEmpty(additionalInformation.AdditionalInformationName))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Додаткова інформація або її властивості не можуть бути пустими." });
            }

            _context.Entry(additionalInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdditionalInformationExists(id))
                {
                    return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено додаткової інформації з таким ID." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/AdditionalInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdditionalInformation>> PostAdditionalInformation(AdditionalInformation additionalInformation)
        {
            if (additionalInformation == null || string.IsNullOrEmpty(additionalInformation.AdditionalInformationName))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Додаткова інформація або її властивості не можуть бути пустими." });
            }

            _context.AdditionalInformations.Add(additionalInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdditionalInformation", new { id = additionalInformation.Id }, additionalInformation);
        }

        // DELETE: api/AdditionalInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdditionalInformation(int id)
        {
            var additionalInformation = await _context.AdditionalInformations.FindAsync(id);
            if (additionalInformation == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено додаткової інформації з таким ID для видалення." });
            }

            _context.AdditionalInformations.Remove(additionalInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdditionalInformationExists(int id)
        {
            return _context.AdditionalInformations.Any(e => e.Id == id);
        }
    }
}
