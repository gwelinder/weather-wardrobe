using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Data;
using WeatherWardrobeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace WeatherWardrobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingItemsController : ControllerBase
    {
        private readonly WeatherWardrobeContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public ClothingItemsController(WeatherWardrobeContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        // GET: api/clothingitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClothingItem>>> GetClothingItems()
        {
            return await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .ToListAsync();
        }

        // GET: api/clothingitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClothingItem>> GetClothingItem(int id)
        {
            var item = await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .FirstOrDefaultAsync(c => c.ClothingItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // GET: api/clothingitems/byweather/{weatherConditionId}
        [HttpGet("byweather/{weatherConditionId}")]
        public async Task<ActionResult<IEnumerable<ClothingItem>>> GetClothingItemsByWeather(int weatherConditionId)
        {
            return await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .Where(c => c.WeatherConditions.Any(w => w.WeatherConditionId == weatherConditionId))
                .ToListAsync();
        }

        // POST: api/clothingitems
        [HttpPost]
        public async Task<ActionResult<ClothingItem>> CreateClothingItem(ClothingItem item)
        {
            _context.ClothingItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClothingItem), new { id = item.ClothingItemId }, item);
        }

        // PUT: api/clothingitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClothingItem(int id, ClothingItem item)
        {
            if (id != item.ClothingItemId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClothingItemExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/clothingitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClothingItem(int id)
        {
            var item = await _context.ClothingItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ClothingItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/clothingitems/recommendations?location=London
        [HttpGet("recommendations")]
        public async Task<ActionResult<IEnumerable<ClothingItem>>> GetRecommendations([FromQuery] string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location is required.");
            }

            // Get the current weather condition based on location
            var weatherCondition = await GetWeatherConditionByLocation(location);

            if (weatherCondition == null)
            {
                return NotFound("Weather condition not found for the given location.");
            }

            // Return clothing items associated with the weather condition
            var clothingItems = await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .Where(c => c.WeatherConditions.Any(w => w.WeatherConditionId == weatherCondition.WeatherConditionId))
                .ToListAsync();

            return Ok(clothingItems);
        }

        private async Task<WeatherCondition> GetWeatherConditionByLocation(string location)
        {
            try
            {
                var (latitude, longitude) = await GetCoordinatesFromCityAsync(location);

                // Call external weather API (Open-Meteo)
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var contentString = await response.Content.ReadAsStringAsync();

                // Deserialize using System.Text.Json
                using var jsonDoc = JsonDocument.Parse(contentString);
                var root = jsonDoc.RootElement;

                // Get the temperature
                double temperature = root.GetProperty("current_weather").GetProperty("temperature").GetDouble();

                // Map temperature to WeatherCondition
                WeatherCondition condition = await GetWeatherConditionByTemperature(temperature);

                return condition;
            }
            catch
            {
                return null;
            }
        }

        private async Task<(double latitude, double longitude)> GetCoordinatesFromCityAsync(string cityName)
        {
            var client = _clientFactory.CreateClient("Nominatim");
            var response = await client.GetAsync($"https://nominatim.openstreetmap.org/search?q={cityName}&format=json&limit=1");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve location data.");
            }

            var contentString = await response.Content.ReadAsStringAsync();

            // Deserialize using System.Text.Json
            using var jsonDoc = JsonDocument.Parse(contentString);
            var root = jsonDoc.RootElement;

            if (root.GetArrayLength() == 0)
            {
                throw new Exception("Location not found.");
            }

            var location = root[0];
            double latitude = double.Parse(location.GetProperty("lat").GetString());
            double longitude = double.Parse(location.GetProperty("lon").GetString());

            return (latitude, longitude);
        }

        private async Task<WeatherCondition> GetWeatherConditionByTemperature(double temperature)
        {
            // Define your own logic to map temperature to WeatherCondition
            if (temperature < 0)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Freezing");
            }
            else if (temperature >= 0 && temperature < 10)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Cold");
            }
            else if (temperature >= 10 && temperature < 15)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Cool");
            }
            else if (temperature >= 15 && temperature < 20)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Mild");
            }
            else if (temperature >= 20 && temperature < 25)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Warm");
            }
            else if (temperature >= 25 && temperature < 30)
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Hot");
            }
            else // temperature >= 30
            {
                return await _context.WeatherConditions
                    .FirstOrDefaultAsync(w => w.ConditionName == "Scorching");
            }
        }

        private bool ClothingItemExists(int id)
        {
            return _context.ClothingItems.Any(e => e.ClothingItemId == id);
        }
    }
}
