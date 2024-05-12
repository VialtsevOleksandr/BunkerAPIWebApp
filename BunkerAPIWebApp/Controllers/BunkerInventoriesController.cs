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
    public class BunkerInventoriesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public BunkerInventoriesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/BunkerInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BunkerInventory>>> GetBunkerInventories()
        {
            return await _context.BunkerInventories.ToListAsync();
        }

        // GET: api/BunkerInventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BunkerInventory>> GetBunkerInventory(int id)
        {
            var bunkerInventory = await _context.BunkerInventories.FindAsync(id);

            if (bunkerInventory == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено інвентаря бункера з таким ID." });
            }

            return bunkerInventory;
        }

        // PUT: api/BunkerInventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBunkerInventory(int id, BunkerInventory bunkerInventory)
        {
            if (id != bunkerInventory.Id)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: ID не відповідає ID інвентаря бункера." });
            }

            if (bunkerInventory == null || string.IsNullOrEmpty(bunkerInventory.BunkerInventoryName))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Інвентар бункера або його властивості не можуть бути пустими." });
            }

            _context.Entry(bunkerInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BunkerInventoryExists(id))
                {
                    return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено інвентаря бункера з таким ID." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BunkerInventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BunkerInventory>> PostBunkerInventory(BunkerInventory bunkerInventory)
        {
            if (bunkerInventory == null || string.IsNullOrEmpty(bunkerInventory.BunkerInventoryName))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Інвентар бункера або його властивості не можуть бути пустими." });
            }

            _context.BunkerInventories.Add(bunkerInventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBunkerInventory", new { id = bunkerInventory.Id }, bunkerInventory);
        }

        // DELETE: api/BunkerInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBunkerInventory(int id)
        {
            var bunkerInventory = await _context.BunkerInventories.FindAsync(id);
            if (bunkerInventory == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено інвентаря бункера з таким ID для видалення." });
            }

            _context.BunkerInventories.Remove(bunkerInventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BunkerInventoryExists(int id)
        {
            return _context.BunkerInventories.Any(e => e.Id == id);
        }
    }
}
