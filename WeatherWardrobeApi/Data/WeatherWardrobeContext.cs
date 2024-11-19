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

            // Seed weather conditions
            modelBuilder.Entity<WeatherCondition>().HasData(
                new WeatherCondition { WeatherConditionId = 1, ConditionName = "Freezing", TemperatureRange = "-20-0" },
                new WeatherCondition { WeatherConditionId = 2, ConditionName = "Cold", TemperatureRange = "0-10" },
                new WeatherCondition { WeatherConditionId = 3, ConditionName = "Cool", TemperatureRange = "10-15" },
                new WeatherCondition { WeatherConditionId = 4, ConditionName = "Mild", TemperatureRange = "15-20" },
                new WeatherCondition { WeatherConditionId = 5, ConditionName = "Warm", TemperatureRange = "20-25" },
                new WeatherCondition { WeatherConditionId = 6, ConditionName = "Hot", TemperatureRange = "25-30" },
                new WeatherCondition { WeatherConditionId = 7, ConditionName = "Scorching", TemperatureRange = "30-45" }
            );
        }
    }
}
