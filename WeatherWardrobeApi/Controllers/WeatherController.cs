using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Data;
using WeatherWardrobeApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace WeatherWardrobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherWardrobeContext _context;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(WeatherWardrobeContext context, ILogger<WeatherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/weather/conditions
        [HttpGet("conditions")]
        [ProducesResponseType(typeof(IEnumerable<WeatherCondition>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<WeatherCondition>>> GetWeatherConditions()
        {
            try
            {
                var conditions = await _context.WeatherConditions
                    .OrderBy(w => w.Temperature)
                    .ToListAsync();

                if (!conditions.Any())
                {
                    return Problem(
                        title: "Server Error",
                        detail: "No weather conditions configured in the system",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Ok(conditions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weather conditions");
                return Problem(
                    title: "Server Error",
                    detail: "An error occurred while retrieving weather conditions",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpGet("recommendations")]
        [ProducesResponseType(typeof(WeatherRecommendation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WeatherRecommendation>> GetWeatherRecommendations([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return Problem(
                    title: "Bad Request",
                    detail: "Location is required",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            try
            {
                // Simulate weather data based on location (just for testing)
                var temperature = GetSimulatedTemperature(location);
                _logger.LogInformation($"Generated temperature {temperature}°C for location: {location}");

                var conditions = await _context.WeatherConditions
                    .Include(w => w.ClothingItems)
                    .OrderBy(w => w.WeatherConditionId)  // Ensure consistent ordering
                    .ToListAsync();

                if (!conditions.Any())
                {
                    _logger.LogWarning("No weather conditions found in database");
                    return Problem(
                        title: "Server Error",
                        detail: "No weather conditions configured in the system",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                // Find the appropriate weather condition based on temperature
                var weatherCondition = FindWeatherConditionForTemperature(conditions, temperature);

                if (weatherCondition == null)
                {
                    var message = $"No weather condition found for temperature {temperature}°C";
                    _logger.LogWarning(message);
                    _logger.LogWarning("Available ranges:");
                    foreach (var condition in conditions)
                    {
                        _logger.LogWarning($"- {condition.ConditionName}: {condition.TemperatureRange}");
                    }
                    return Problem(
                        title: "Not Found",
                        detail: message,
                        statusCode: StatusCodes.Status404NotFound
                    );
                }

                var clothingItems = weatherCondition.ClothingItems;

                if (!clothingItems.Any())
                {
                    var message = $"No clothing recommendations found for {weatherCondition.ConditionName} conditions";
                    _logger.LogWarning(message);
                    return Problem(
                        title: "Not Found",
                        detail: message,
                        statusCode: StatusCodes.Status404NotFound
                    );
                }

                var recommendation = new WeatherRecommendation
                {
                    WeatherCondition = weatherCondition,
                    Temperature = temperature,
                    Location = location,
                    RecommendedItems = clothingItems.ToList()
                };

                return Ok(recommendation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing recommendations for location: {location}");
                return Problem(
                    title: "Server Error",
                    detail: "An error occurred while processing your request",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        private WeatherCondition? FindWeatherConditionForTemperature(IEnumerable<WeatherCondition> conditions, double temperature)
        {
            _logger.LogInformation($"Finding weather condition for temperature: {temperature}°C");
            foreach (var condition in conditions.OrderBy(c => c.WeatherConditionId))  // Process in order
            {
                if (TryParseTemperatureRange(condition.TemperatureRange, out double min, out double max))
                {
                    _logger.LogInformation($"Checking range {condition.ConditionName}: {min} to {max} for temperature {temperature}");
                    // Use inclusive range for both min and max
                    if (temperature >= min && temperature <= max)  // Changed to inclusive on both ends
                    {
                        _logger.LogInformation($"Found matching condition: {condition.ConditionName}");
                        return condition;
                    }
                }
                else
                {
                    _logger.LogWarning($"Failed to parse temperature range: {condition.TemperatureRange}");
                }
            }

            // Log all available ranges when no match is found
            _logger.LogWarning($"No matching condition found for temperature {temperature}°C");
            _logger.LogWarning("Available ranges:");
            foreach (var condition in conditions)
            {
                _logger.LogWarning($"- {condition.ConditionName}: {condition.TemperatureRange}");
            }
            return null;
        }

        private double GetSimulatedTemperature(string location)
        {
            // Use SHA256 to get a consistent hash
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(location.ToLowerInvariant()));
            
            // Take first 4 bytes and convert to integer
            var hashValue = BitConverter.ToInt32(hashBytes, 0);
            
            // Normalize to range -40 to 40 degrees Celsius (total range of 80 degrees)
            var normalizedTemp = (Math.Abs(hashValue) % 800) / 10.0 - 40.0;
            
            // Round to one decimal place
            return Math.Round(normalizedTemp, 1);
        }

        private bool TryParseTemperatureRange(string range, out double min, out double max)
        {
            min = max = 0;
            try
            {
                // Try the new "to" format first
                var parts = range.Split(" to ", StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    if (!double.TryParse(parts[0], out min)) return false;
                    if (!double.TryParse(parts[1], out max)) return false;
                }
                else
                {
                    // Try the old dash format as fallback
                    parts = range.Split('-');
                    if (parts.Length < 2) return false;

                    // Handle negative numbers
                    if (parts.Length == 3)
                    {
                        // Case like "-40--30" or "-40-30"
                        if (!double.TryParse("-" + parts[1], out min)) return false;
                        if (!double.TryParse(parts[2], out max)) return false;
                    }
                    else
                    {
                        // Case like "0-10"
                        if (!double.TryParse(parts[0], out min)) return false;
                        if (!double.TryParse(parts[1], out max)) return false;
                    }
                }

                _logger.LogInformation($"Parsed temperature range: {min} to {max} from {range}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to parse temperature range: {range}");
                return false;
            }
        }
    }
}
