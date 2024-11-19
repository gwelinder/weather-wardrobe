using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherWardrobeApi.Models
{
    public class ClothingItem
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _imageUrl = string.Empty;
        private int[]? _weatherConditionIds;
        private ICollection<WeatherCondition> _weatherConditions = new List<WeatherCondition>();

        public ClothingItem()
        {
        }

        [Key]
        public int ClothingItemId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name 
        {
            get => _name;
            set => _name = value ?? string.Empty;
        }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description 
        {
            get => _description;
            set => _description = value ?? string.Empty;
        }

        [Required(ErrorMessage = "Image URL is required")]
        [StringLength(1000, ErrorMessage = "Image URL cannot be longer than 1000 characters")]
        public string ImageURL 
        {
            get => _imageUrl;
            set => _imageUrl = value ?? string.Empty;
        }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public virtual ICollection<WeatherCondition> WeatherConditions 
        { 
            get => _weatherConditions;
            set => _weatherConditions = value ?? new List<WeatherCondition>();
        }

        // Helper property for binding weather condition IDs
        [NotMapped]
        public int[]? WeatherConditionIds 
        { 
            get => _weatherConditionIds ?? WeatherConditions?.Select(w => w.WeatherConditionId).ToArray();
            set => _weatherConditionIds = value;
        }
    }
}
