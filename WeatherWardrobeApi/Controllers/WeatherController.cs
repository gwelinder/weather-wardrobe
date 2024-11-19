using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Data;
using WeatherWardrobeApi.Models;

namespace WeatherWardrobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherWardrobeContext _context;

        public WeatherController(WeatherWardrobeContext context)
        {
            _context = context;
        }

        [HttpGet("recommendations")]
        public async Task<ActionResult<WeatherRecommendation>> GetWeatherRecommendations([FromQuery] string location)
        {
            // Simulate weather data based on location (just for testing)
            var temperature = GetSimulatedTemperature(location);
            
            // Find the appropriate weather condition based on temperature
            var weatherCondition = await FindWeatherConditionForTemperature(temperature);

            if (weatherCondition == null)
            {
                return NotFound($"No weather condition found for temperature {temperature}°C");
            }

            // Get clothing recommendations for this weather condition
            var clothingItems = await _context.ClothingItems
                .Include(c => c.WeatherConditions)
                .Where(c => c.WeatherConditions.Any(w => w.WeatherConditionId == weatherCondition.WeatherConditionId))
                .ToListAsync();

            return new WeatherRecommendation
            {
                WeatherCondition = weatherCondition,
                Temperature = temperature,
                Location = location,
                RecommendedItems = clothingItems
            };
        }

        private async Task<WeatherCondition?> FindWeatherConditionForTemperature(double temperature)
        {
            var conditions = await _context.WeatherConditions.ToListAsync();
            foreach (var condition in conditions)
            {
                if (TryParseTemperatureRange(condition.TemperatureRange, out double min, out double max) &&
                    temperature >= min && temperature < max)
                {
                    return condition;
                }
            }
            return null;
        }

        private double GetSimulatedTemperature(string location)
        {
            // Simple hash function to generate consistent temperatures for locations
            var hash = location.GetHashCode();
            var normalizedHash = (hash % 500) / 10.0; // Range from -25 to 25
            return normalizedHash;
        }

        private bool TryParseTemperatureRange(string range, out double min, out double max)
        {
            min = max = 0;
            var parts = range.Split('-');
            if (parts.Length != 2) return false;
            
            return double.TryParse(parts[0], out min) && double.TryParse(parts[1], out max);
        }
    }
}
