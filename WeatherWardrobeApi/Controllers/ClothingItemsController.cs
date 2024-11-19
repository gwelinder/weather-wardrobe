using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Data;
using WeatherWardrobeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace WeatherWardrobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingItemsController : ControllerBase
    {
        private readonly WeatherWardrobeContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ClothingItemsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ClothingItemsController(WeatherWardrobeContext context, IHttpClientFactory clientFactory, ILogger<ClothingItemsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _clientFactory = clientFactory;
            _logger = logger;
            _environment = environment;
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
        public async Task<ActionResult<ClothingItem>> CreateClothingItem([FromBody] ClothingItem item)
        {
            try 
            {
                _logger.LogInformation("Creating clothing item with properties: Name={Name}, Description={Description}, ImageURL={ImageURL}, WeatherConditionIds={WeatherConditionIds}", 
                    item?.Name,
                    item?.Description,
                    item?.ImageURL,
                    item?.WeatherConditionIds != null ? string.Join(",", item.WeatherConditionIds) : "null");

                if (item == null)
                {
                    _logger.LogWarning("Received null item");
                    return BadRequest(new { errors = new { General = new[] { "Invalid request body" } } });
                }

                // Handle weather conditions before model validation
                if (item.WeatherConditionIds?.Any() == true)
                {
                    _logger.LogInformation("Processing {Count} weather condition IDs: {Ids}", 
                        item.WeatherConditionIds.Length, 
                        string.Join(",", item.WeatherConditionIds));

                    var weatherConditions = await _context.WeatherConditions
                        .Where(w => item.WeatherConditionIds.Contains(w.WeatherConditionId))
                        .ToListAsync();

                    _logger.LogInformation("Found {Count} matching weather conditions in database", weatherConditions.Count);
                    foreach (var wc in weatherConditions)
                    {
                        _logger.LogInformation("Weather condition found: Id={Id}, Name={Name}", 
                            wc.WeatherConditionId, 
                            wc.ConditionName);
                    }

                    if (!weatherConditions.Any())
                    {
                        return BadRequest(new { errors = new { WeatherConditions = new[] { "No valid weather conditions found" } } });
                    }

                    item.WeatherConditions = weatherConditions;
                    _logger.LogInformation("Assigned {Count} weather conditions to item", item.WeatherConditions.Count);
                }
                else
                {
                    _logger.LogWarning("No weather condition IDs provided in the request");
                    return BadRequest(new { errors = new { WeatherConditions = new[] { "At least one weather condition is required" } } });
                }

                // Create a new clothing item without the weather conditions first
                var newItem = new ClothingItem
                {
                    Name = item.Name,
                    Description = item.Description,
                    ImageURL = item.ImageURL,
                    WeatherConditions = item.WeatherConditions
                };

                _context.ClothingItems.Add(newItem);
                await _context.SaveChangesAsync();

                // Reload the item with weather conditions included
                var savedItem = await _context.ClothingItems
                    .Include(c => c.WeatherConditions)
                    .FirstOrDefaultAsync(c => c.ClothingItemId == newItem.ClothingItemId);

                return CreatedAtAction(nameof(GetClothingItem), new { id = savedItem.ClothingItemId }, savedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating clothing item");
                return StatusCode(500, new { errors = new { General = new[] { "An error occurred while saving the item" } } });
            }
        }

        // PUT: api/clothingitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClothingItem(int id, ClothingItem item)
        {
            if (id != item.ClothingItemId)
            {
                return BadRequest();
            }

            var existingItem = await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .FirstOrDefaultAsync(c => c.ClothingItemId == id);

            if (existingItem == null)
            {
                return NotFound();
            }

            // Update basic properties
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.ImageURL = item.ImageURL;

            // Update weather conditions
            existingItem.WeatherConditions.Clear();
            if (item.WeatherConditions != null && item.WeatherConditions.Any())
            {
                var weatherConditionIds = item.WeatherConditions.Select(w => w.WeatherConditionId).ToList();
                var weatherConditions = await _context.WeatherConditions
                    .Where(w => weatherConditionIds.Contains(w.WeatherConditionId))
                    .ToListAsync();
                
                foreach (var condition in weatherConditions)
                {
                    existingItem.WeatherConditions.Add(condition);
                }
            }

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

        // POST: api/clothingitems/images/upload
        [HttpPost("images/upload")]
        public async Task<ActionResult<object>> UploadImage([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                // Validate file type
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(file.ContentType.ToLower()))
                {
                    return BadRequest("Invalid file type. Only JPEG, PNG and GIF files are allowed.");
                }

                // Create uploads directory if it doesn't exist
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the URL to access the file
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var imageUrl = $"{baseUrl}/uploads/{fileName}";

                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image");
                return StatusCode(500, "Error uploading image");
            }
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
