using WeatherWardrobeApi.Models;
using WeatherWardrobeApi.Data;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("Nominatim", client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "WeatherWardrobeApp/1.0 (yourcontactinfo@example.com)");
});

// Register DbContext with connection string
builder.Services.AddDbContext<WeatherWardrobeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WeatherWardrobeContext>();
    // Ensure database is created
    context.Database.EnsureCreated();

    // Seed data if necessary
    if (!context.WeatherConditions.Any())
    {
        // Define Weather Conditions
        var freezingCondition = new WeatherCondition
        {
            ConditionName = "Freezing",
            TemperatureRange = "Below 0°C",
            ClothingItems = new List<ClothingItem>()
        };

        var coldCondition = new WeatherCondition
        {
            ConditionName = "Cold",
            TemperatureRange = "0°C - 10°C",
            ClothingItems = new List<ClothingItem>()
        };

        var coolCondition = new WeatherCondition
        {
            ConditionName = "Cool",
            TemperatureRange = "10°C - 15°C",
            ClothingItems = new List<ClothingItem>()
        };

        var mildCondition = new WeatherCondition
        {
            ConditionName = "Mild",
            TemperatureRange = "15°C - 20°C",
            ClothingItems = new List<ClothingItem>()
        };

        var warmCondition = new WeatherCondition
        {
            ConditionName = "Warm",
            TemperatureRange = "20°C - 25°C",
            ClothingItems = new List<ClothingItem>()
        };

        var hotCondition = new WeatherCondition
        {
            ConditionName = "Hot",
            TemperatureRange = "25°C - 30°C",
            ClothingItems = new List<ClothingItem>()
        };

        var scorchingCondition = new WeatherCondition
        {
            ConditionName = "Scorching",
            TemperatureRange = "Above 30°C",
            ClothingItems = new List<ClothingItem>()
        };

        context.WeatherConditions.AddRange(freezingCondition, coldCondition, coolCondition, mildCondition, warmCondition, hotCondition, scorchingCondition);
        context.SaveChanges();

        // Seed initial clothing items
        var winterJacket = new ClothingItem
        {
            ClothingItemId = 1,
            Name = "Winter Jacket",
            Description = "Heavy insulated jacket for cold weather",
            ImageURL = "winter-jacket.jpg"
        };
        winterJacket.WeatherConditions = new List<WeatherCondition> { freezingCondition, coldCondition };

        var lightJacket = new ClothingItem
        {
            ClothingItemId = 2,
            Name = "Light Jacket",
            Description = "Light jacket for cool weather",
            ImageURL = "light-jacket.jpg"
        };
        lightJacket.WeatherConditions = new List<WeatherCondition> { coolCondition, mildCondition };

        var tShirt = new ClothingItem
        {
            ClothingItemId = 3,
            Name = "T-Shirt",
            Description = "Basic t-shirt for warm weather",
            ImageURL = "tshirt.jpg"
        };
        tShirt.WeatherConditions = new List<WeatherCondition> { warmCondition, hotCondition };

        context.ClothingItems.AddRange(winterJacket, lightJacket, tShirt);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

// Use Authentication Middleware
app.UseMiddleware<BasicAuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
