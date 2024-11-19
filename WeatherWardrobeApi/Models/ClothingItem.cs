using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace WeatherWardrobeApi.Models
{
    public class ClothingItem
    {
        public ClothingItem()
        {
            Name = string.Empty;
            Description = string.Empty;
            ImageURL = string.Empty;
            WeatherConditions = new List<WeatherCondition>();
        }

        [Key]
        public int ClothingItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
    }
}
