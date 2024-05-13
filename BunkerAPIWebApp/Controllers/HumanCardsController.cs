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
    public class HumanCardsController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public HumanCardsController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/HumanCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HumanCard>>> GetHumanCards()
        {
            return await _context.HumanCards.ToListAsync();
        }

        // GET: api/HumanCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HumanCard>> GetHumanCard(int id)
        {
            var humanCard = await _context.HumanCards.FindAsync(id);

            if (humanCard == null)
            {
                return NotFound();
            }

            return humanCard;
        }

        // PUT: api/HumanCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumanCard(int id, HumanCard humanCard)
        {
            if (id != humanCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(humanCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanCardExists(id))
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

        // POST: api/HumanCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HumanCard>> PostHumanCard(HumanCard humanCard)
        {
            _context.HumanCards.Add(humanCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHumanCard", new { id = humanCard.Id }, humanCard);
        }

        // DELETE: api/HumanCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHumanCard(int id)
        {
            var humanCard = await _context.HumanCards.FindAsync(id);
            if (humanCard == null)
            {
                return NotFound();
            }

            _context.HumanCards.Remove(humanCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanCardExists(int id)
        {
            return _context.HumanCards.Any(e => e.Id == id);
        }
    }
}
