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
    public class FoodQuantitiesController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public FoodQuantitiesController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/FoodQuantities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodQuantity>>> GetFoodQuantities()
        {
            return await _context.FoodQuantities.ToListAsync();
        }

        // GET: api/FoodQuantities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodQuantity>> GetFoodQuantity(int id)
        {
            var foodQuantity = await _context.FoodQuantities.FindAsync(id);

            if (foodQuantity == null)
            {
                return NotFound();
            }

            return foodQuantity;
        }

        // PUT: api/FoodQuantities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodQuantity(int id, FoodQuantity foodQuantity)
        {
            if (id != foodQuantity.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodQuantity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodQuantityExists(id))
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

        // POST: api/FoodQuantities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodQuantity>> PostFoodQuantity(FoodQuantity foodQuantity)
        {
            _context.FoodQuantities.Add(foodQuantity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodQuantity", new { id = foodQuantity.Id }, foodQuantity);
        }

        // DELETE: api/FoodQuantities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodQuantity(int id)
        {
            var foodQuantity = await _context.FoodQuantities.FindAsync(id);
            if (foodQuantity == null)
            {
                return NotFound();
            }

            _context.FoodQuantities.Remove(foodQuantity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodQuantityExists(int id)
        {
            return _context.FoodQuantities.Any(e => e.Id == id);
        }
    }
}
