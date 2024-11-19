using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WeatherWardrobeApi.Models
{
    public class WeatherCondition
    {
        public WeatherCondition()
        {
            ClothingItems = new List<ClothingItem>();
            ConditionName = string.Empty;
            TemperatureRange = string.Empty;
        }

        [Key]
        public int WeatherConditionId { get; set; }

        [Required]
        public string ConditionName { get; set; }

        [Required]
        public string TemperatureRange { get; set; }
        
        public double Temperature { get; set; }

        [JsonIgnore]
        public virtual ICollection<ClothingItem> ClothingItems { get; set; }
    }

    public class WeatherRecommendation
    {
        public WeatherRecommendation()
        {
            WeatherCondition = new WeatherCondition();
            Location = string.Empty;
            RecommendedItems = new List<ClothingItem>();
        }

        public WeatherCondition WeatherCondition { get; set; }
        public double Temperature { get; set; }
        public string Location { get; set; }
        public ICollection<ClothingItem> RecommendedItems { get; set; }
    }
}
