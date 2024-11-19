using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Models;

namespace WeatherWardrobeApi.Data
{
    public class WeatherWardrobeContext : DbContext
    {
        public WeatherWardrobeContext(DbContextOptions<WeatherWardrobeContext> options)
            : base(options)
        {
        }

        public DbSet<ClothingItem> ClothingItems { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
            modelBuilder.Entity<ClothingItem>()
                .HasMany(c => c.WeatherConditions)
                .WithMany(w => w.ClothingItems)
                .UsingEntity(j => j.ToTable("ClothingItemWeatherCondition"));

            // Seed weather conditions with overlapping ranges to ensure no gaps
            modelBuilder.Entity<WeatherCondition>().HasData(
                new WeatherCondition { WeatherConditionId = 1, ConditionName = "Arctic", TemperatureRange = "-40 to -30" },
                new WeatherCondition { WeatherConditionId = 2, ConditionName = "Extreme Cold", TemperatureRange = "-30 to -20" },
                new WeatherCondition { WeatherConditionId = 3, ConditionName = "Very Cold", TemperatureRange = "-20 to -10" },
                new WeatherCondition { WeatherConditionId = 4, ConditionName = "Cold", TemperatureRange = "-10 to 0" },
                new WeatherCondition { WeatherConditionId = 5, ConditionName = "Chilly", TemperatureRange = "0 to 10" },
                new WeatherCondition { WeatherConditionId = 6, ConditionName = "Cool", TemperatureRange = "10 to 15" },
                new WeatherCondition { WeatherConditionId = 7, ConditionName = "Mild", TemperatureRange = "15 to 20" },
                new WeatherCondition { WeatherConditionId = 8, ConditionName = "Warm", TemperatureRange = "20 to 25" },
                new WeatherCondition { WeatherConditionId = 9, ConditionName = "Hot", TemperatureRange = "25 to 30" },
                new WeatherCondition { WeatherConditionId = 10, ConditionName = "Very Hot", TemperatureRange = "30 to 40" }  
            );

            // Seed clothing items with additional winter gear
            modelBuilder.Entity<ClothingItem>().HasData(
                // Existing items
                new ClothingItem { ClothingItemId = 1, Name = "Winter Coat", Description = "Heavy insulated winter coat", ImageURL = "coat.jpg" },
                new ClothingItem { ClothingItemId = 2, Name = "Light Jacket", Description = "Light windbreaker jacket", ImageURL = "jacket.jpg" },
                new ClothingItem { ClothingItemId = 3, Name = "T-Shirt", Description = "Cotton t-shirt", ImageURL = "tshirt.jpg" },
                new ClothingItem { ClothingItemId = 4, Name = "Sweater", Description = "Warm wool sweater", ImageURL = "sweater.jpg" },
                new ClothingItem { ClothingItemId = 5, Name = "Shorts", Description = "Casual shorts", ImageURL = "shorts.jpg" },
                new ClothingItem { ClothingItemId = 6, Name = "Long Pants", Description = "Regular fit jeans", ImageURL = "pants.jpg" },
                new ClothingItem { ClothingItemId = 7, Name = "Winter Hat", Description = "Warm beanie", ImageURL = "hat.jpg" },
                new ClothingItem { ClothingItemId = 8, Name = "Sandals", Description = "Summer sandals", ImageURL = "sandals.jpg" },
                new ClothingItem { ClothingItemId = 9, Name = "Winter Boots", Description = "Insulated winter boots", ImageURL = "boots.jpg" },
                new ClothingItem { ClothingItemId = 10, Name = "Tank Top", Description = "Light sleeveless top", ImageURL = "tank.jpg" },
                // New extreme weather items
                new ClothingItem { ClothingItemId = 11, Name = "Thermal Underwear", Description = "Base layer thermal underwear", ImageURL = "thermal.jpg" },
                new ClothingItem { ClothingItemId = 12, Name = "Snow Pants", Description = "Insulated snow pants", ImageURL = "snowpants.jpg" },
                new ClothingItem { ClothingItemId = 13, Name = "Arctic Parka", Description = "Extreme cold weather parka", ImageURL = "parka.jpg" },
                new ClothingItem { ClothingItemId = 14, Name = "Neck Gaiter", Description = "Thermal neck protection", ImageURL = "gaiter.jpg" },
                new ClothingItem { ClothingItemId = 15, Name = "Insulated Gloves", Description = "Heavy-duty winter gloves", ImageURL = "gloves.jpg" }
            );

            // Seed clothing item - weather condition relationships
            modelBuilder.Entity("ClothingItemWeatherCondition").HasData(
                // Arctic Parka (Arctic, Extreme Cold)
                new { ClothingItemsClothingItemId = 13, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 13, WeatherConditionsWeatherConditionId = 2 },

                // Thermal Underwear (Arctic through Very Cold)
                new { ClothingItemsClothingItemId = 11, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 11, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 11, WeatherConditionsWeatherConditionId = 3 },

                // Snow Pants (Arctic through Cold)
                new { ClothingItemsClothingItemId = 12, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 12, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 12, WeatherConditionsWeatherConditionId = 3 },
                new { ClothingItemsClothingItemId = 12, WeatherConditionsWeatherConditionId = 4 },

                // Neck Gaiter (Arctic through Very Cold)
                new { ClothingItemsClothingItemId = 14, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 14, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 14, WeatherConditionsWeatherConditionId = 3 },

                // Insulated Gloves (Arctic through Cold)
                new { ClothingItemsClothingItemId = 15, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 15, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 15, WeatherConditionsWeatherConditionId = 3 },
                new { ClothingItemsClothingItemId = 15, WeatherConditionsWeatherConditionId = 4 },

                // Winter Coat (Very Cold through Chilly)
                new { ClothingItemsClothingItemId = 1, WeatherConditionsWeatherConditionId = 3 },
                new { ClothingItemsClothingItemId = 1, WeatherConditionsWeatherConditionId = 4 },
                new { ClothingItemsClothingItemId = 1, WeatherConditionsWeatherConditionId = 5 },
                
                // Light Jacket (Cool, Mild)
                new { ClothingItemsClothingItemId = 2, WeatherConditionsWeatherConditionId = 6 },
                new { ClothingItemsClothingItemId = 2, WeatherConditionsWeatherConditionId = 7 },
                
                // T-Shirt (Mild through Very Hot)
                new { ClothingItemsClothingItemId = 3, WeatherConditionsWeatherConditionId = 7 },
                new { ClothingItemsClothingItemId = 3, WeatherConditionsWeatherConditionId = 8 },
                new { ClothingItemsClothingItemId = 3, WeatherConditionsWeatherConditionId = 9 },
                new { ClothingItemsClothingItemId = 3, WeatherConditionsWeatherConditionId = 10 },
                
                // Sweater (Chilly, Cool)
                new { ClothingItemsClothingItemId = 4, WeatherConditionsWeatherConditionId = 5 },
                new { ClothingItemsClothingItemId = 4, WeatherConditionsWeatherConditionId = 6 },
                
                // Shorts (Warm through Very Hot)
                new { ClothingItemsClothingItemId = 5, WeatherConditionsWeatherConditionId = 8 },
                new { ClothingItemsClothingItemId = 5, WeatherConditionsWeatherConditionId = 9 },
                new { ClothingItemsClothingItemId = 5, WeatherConditionsWeatherConditionId = 10 },
                
                // Long Pants (Cold through Mild)
                new { ClothingItemsClothingItemId = 6, WeatherConditionsWeatherConditionId = 4 },
                new { ClothingItemsClothingItemId = 6, WeatherConditionsWeatherConditionId = 5 },
                new { ClothingItemsClothingItemId = 6, WeatherConditionsWeatherConditionId = 6 },
                new { ClothingItemsClothingItemId = 6, WeatherConditionsWeatherConditionId = 7 },
                
                // Winter Hat (Arctic through Cold)
                new { ClothingItemsClothingItemId = 7, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 7, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 7, WeatherConditionsWeatherConditionId = 3 },
                new { ClothingItemsClothingItemId = 7, WeatherConditionsWeatherConditionId = 4 },
                
                // Sandals (Warm through Very Hot)
                new { ClothingItemsClothingItemId = 8, WeatherConditionsWeatherConditionId = 8 },
                new { ClothingItemsClothingItemId = 8, WeatherConditionsWeatherConditionId = 9 },
                new { ClothingItemsClothingItemId = 8, WeatherConditionsWeatherConditionId = 10 },
                
                // Winter Boots (Arctic through Cold)
                new { ClothingItemsClothingItemId = 9, WeatherConditionsWeatherConditionId = 1 },
                new { ClothingItemsClothingItemId = 9, WeatherConditionsWeatherConditionId = 2 },
                new { ClothingItemsClothingItemId = 9, WeatherConditionsWeatherConditionId = 3 },
                new { ClothingItemsClothingItemId = 9, WeatherConditionsWeatherConditionId = 4 },
                
                // Tank Top (Hot, Very Hot)
                new { ClothingItemsClothingItemId = 10, WeatherConditionsWeatherConditionId = 9 },
                new { ClothingItemsClothingItemId = 10, WeatherConditionsWeatherConditionId = 10 }
            );
        }
    }
}
