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
    public class GameSettingsController : ControllerBase
    {
        private readonly BunkerAPIContext _context;

        public GameSettingsController(BunkerAPIContext context)
        {
            _context = context;
        }

        // GET: api/GameSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSetting>>> GetGameSettings()
        {
            return await _context.GameSettings.ToListAsync();
        }

        // GET: api/GameSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSetting>> GetGameSetting(int id)
        {
            var gameSetting = await _context.GameSettings
              .Include(gs => gs.GameSettingHumanCards)
              .SingleOrDefaultAsync(gs => gs.Id == id);

            if (gameSetting == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено гри з таким ID." });
            }

            return gameSetting;
        }

        // PUT: api/GameSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutGameSetting(int id, GameSetting gameSetting)
        {
            return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Помилка, за правилами нашого сайту не можна вносити зміни в вже створені ігри" });
        }

        // POST: api/GameSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameSetting>> PostGameSetting(GameSetting gameSetting)
        {
            int playerCount = gameSetting.CountOfPlayers;
            if (gameSetting == null)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Налаштування гри не може бути пустими." });
            }

            if (playerCount <= 2 || playerCount > 20)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Кількість гравців повинна бути від 2 до 20." });
            }

            var catastropheIds = await GetAllCatastropheIds();
            if (catastropheIds.Count == 0) 
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Недостатньо карт катастроф для початку гри. Будь ласка, зверністься до адміністратора." });
            }

            if (!_context.BunkerSizes.Any() || !_context.ResidenceTimes.Any() || !_context.FoodQuantities.Any() || !_context.BunkerInventories.Any())
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Недостатньо карт, що відносяться до бункера, для початку гри. Будь ласка, зверністься до адміністратора." });
            }

            if (_context.AdditionalInformations.Count() < playerCount || _context.Genders.Count() < playerCount || _context.Healths.Count() < playerCount || _context.Hobbies.Count() < playerCount || _context.HumanTraits.Count() < playerCount || _context.Inventories.Count() < playerCount || _context.Phobias.Count() < playerCount || _context.Professions.Count() < playerCount || _context.SpecialFeatures.Count() < playerCount)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Невірний запит: Недостатньо карт, що відносяться до картки людини, для початку гри. Будь ласка, зверніться до адміністратора." });
            }

            var bunker = new Bunker
            {
                BunkerSizeId = GetRandomIdFromList(await GetAllBunkerSizeIds()),
                ResidenceTimeId = GetRandomIdFromList(await GetAllResidenceTimeIds()),
                FoodQuantityId = GetRandomIdFromList(await GetAllFoodQuantityIds()),
                BunkerInventoryId = GetRandomIdFromList(await GetAllBunkerInventoryIds())
            };
            if (!_context.Bunkers.Any(b => b.BunkerSizeId == bunker.BunkerSizeId && b.ResidenceTimeId == bunker.ResidenceTimeId && b.FoodQuantityId == bunker.FoodQuantityId && b.BunkerInventoryId == bunker.BunkerInventoryId))
            {
                _context.Bunkers.Add(bunker);
                await _context.SaveChangesAsync();
            }
            else
            {
                bunker = _context.Bunkers.First(b => b.BunkerSizeId == bunker.BunkerSizeId && b.ResidenceTimeId == bunker.ResidenceTimeId && b.FoodQuantityId == bunker.FoodQuantityId && b.BunkerInventoryId == bunker.BunkerInventoryId);
            }
            gameSetting.BunkerId = bunker.Id;
            gameSetting.CatastropheId = GetRandomIdFromList(catastropheIds);
            
            _context.GameSettings.Add(gameSetting);
            await _context.SaveChangesAsync();

            var additionalInformationIds = await GetAllAdditionalInformationIds();
            var genderIds = await GetAllGenderIds();
            var healthIds = await GetAllHealthIds();
            var hobbyIds = await GetAllHobbyIds();
            var humanTraitIds = await GetAllHumanTraitIds();
            var inventoryIds = await GetAllInventoryIds();
            var phobiaIds = await GetAllPhobiaIds();
            var professionIds = await GetAllProfessionIds();
            var specialFeatureIds = await GetAllSpecialFeatureIds();

            for (int i = 0; i < playerCount; i++)
            {
                var genderId = GetRandomIdFromList(genderIds);
                var validProfessionIds = await GetProfessionIdsBasedOnAge(_context.Genders.Find(genderId).Age, professionIds);

                if (!validProfessionIds.Any())
                {
                    return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Немає доступних професій, які відповідають віковим вимогам. Будь ласка, зверніться до адміністратора." });
                }

                var humanCard = new HumanCard
                {
                    AdditionalInformationId = GetRandomIdFromList(additionalInformationIds),
                    GenderId = genderId,
                    HealthId = GetRandomIdFromList(healthIds),
                    HobbyId = GetRandomIdFromList(hobbyIds),
                    HumanTraitId = GetRandomIdFromList(humanTraitIds),
                    InventoryId = GetRandomIdFromList(inventoryIds),
                    PhobiaId = GetRandomIdFromList(phobiaIds),
                    ProfessionId = GetRandomIdFromList(validProfessionIds),
                    SpecialFeatureId = GetRandomIdFromList(specialFeatureIds)
                };
                if (!_context.HumanCards.Any(hc => hc.AdditionalInformationId == humanCard.AdditionalInformationId && hc.GenderId == humanCard.GenderId && hc.HealthId == humanCard.HealthId && hc.HobbyId == humanCard.HobbyId && hc.HumanTraitId == humanCard.HumanTraitId && hc.InventoryId == humanCard.InventoryId && hc.PhobiaId == humanCard.PhobiaId && hc.ProfessionId == humanCard.ProfessionId && hc.SpecialFeatureId == humanCard.SpecialFeatureId))
                {
                    _context.HumanCards.Add(humanCard);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    humanCard = _context.HumanCards.First(hc => hc.AdditionalInformationId == humanCard.AdditionalInformationId && hc.GenderId == humanCard.GenderId && hc.HealthId == humanCard.HealthId && hc.HobbyId == humanCard.HobbyId && hc.HumanTraitId == humanCard.HumanTraitId && hc.InventoryId == humanCard.InventoryId && hc.PhobiaId == humanCard.PhobiaId && hc.ProfessionId == humanCard.ProfessionId && hc.SpecialFeatureId == humanCard.SpecialFeatureId);
                }
                additionalInformationIds.Remove(humanCard.AdditionalInformationId);
                genderIds.Remove(humanCard.GenderId);
                healthIds.Remove(humanCard.HealthId);
                hobbyIds.Remove(humanCard.HobbyId);
                humanTraitIds.Remove(humanCard.HumanTraitId);
                inventoryIds.Remove(humanCard.InventoryId);
                phobiaIds.Remove(humanCard.PhobiaId);
                professionIds.Remove(humanCard.ProfessionId);
                specialFeatureIds.Remove(humanCard.SpecialFeatureId);

                var gameSettingHumanCard = new GameSettingHumanCard
                {
                    GameSettingId = gameSetting.Id,
                    HumanCardId = humanCard.Id
                };
                _context.GameSettingHumanCards.Add(gameSettingHumanCard);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetGameSetting", new { id = gameSetting.Id }, gameSetting);
        }

        // DELETE: api/GameSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSetting(int id)
        {
            var gameSetting = await _context.GameSettings.FindAsync(id);
            if (gameSetting == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Не знайдено гри з таким ID для видалення." });
            }

            _context.GameSettings.Remove(gameSetting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameSettingExists(int id)
        {
            return _context.GameSettings.Any(e => e.Id == id);
        }

        public int GetRandomIdFromList(List<int> ids)
        {
            var random = new Random();
            return ids[random.Next(ids.Count)];
        }
        
        public async Task<List<int>> GetAllAdditionalInformationIds()
        {
            return await _context.AdditionalInformations.Select(ai => ai.Id).ToListAsync();
        }
        public async Task<List<int>> GetAllGenderIds()
        {
            return await _context.Genders.Select(g => g.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllHealthIds()
        {
            return await _context.Healths.Select(h => h.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllHobbyIds()
        {
            return await _context.Hobbies.Select(h => h.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllHumanTraitIds()
        {
            return await _context.HumanTraits.Select(ht => ht.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllInventoryIds()
        {
            return await _context.Inventories.Select(i => i.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllPhobiaIds()
        {
            return await _context.Phobias.Select(p => p.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllProfessionIds()
        {
            return await _context.Professions.Select(p => p.Id).ToListAsync();
        }
        public async Task<List<int>> GetProfessionIdsBasedOnAge(byte age, List<int> availableProfessionIds)
        {
            var professions = await _context.Professions.Where(p => availableProfessionIds.Contains(p.Id)).ToListAsync();
            var validProfessionIds = new List<int>();

            foreach (var profession in professions)
            {
                if (age - 18 >= profession.WorkExperience)
                {
                    validProfessionIds.Add(profession.Id);
                }
            }

            return validProfessionIds;
        }

        public async Task<List<int>> GetAllSpecialFeatureIds()
        {
            return await _context.SpecialFeatures.Select(sf => sf.Id).ToListAsync();
        }
        public async Task<List<int>> GetAllBunkerSizeIds()
        {
            return await _context.BunkerSizes.Select(bs => bs.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllResidenceTimeIds()
        {
            return await _context.ResidenceTimes.Select(rt => rt.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllFoodQuantityIds()
        {
            return await _context.FoodQuantities.Select(fq => fq.Id).ToListAsync();
        }

        public async Task<List<int>> GetAllBunkerInventoryIds()
        {
            return await _context.BunkerInventories.Select(bi => bi.Id).ToListAsync();
        }
        public async Task<List<int>> GetAllCatastropheIds()
        {
            return await _context.Catastrophes.Select(hc => hc.Id).ToListAsync();
        }
    }
}
