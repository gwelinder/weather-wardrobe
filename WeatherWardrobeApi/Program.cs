using WeatherWardrobeApi.Models;
using WeatherWardrobeApi.Data;
using Microsoft.EntityFrameworkCore;
using WeatherWardrobeApi.Middleware;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            return new BadRequestObjectResult(problemDetails);
        };
    });

// Configure ProblemDetails
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState);
        problemDetails.Status = StatusCodes.Status400BadRequest;
        return new BadRequestObjectResult(problemDetails);
    };
});

builder.Services.AddProblemDetails();

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
            .AllowAnyOrigin()  // For development only
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add static files middleware before authorization and routing
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseMiddleware<BasicAuthenticationMiddleware>();
app.UseAuthorization();
app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WeatherWardrobeContext>();
    context.Database.EnsureCreated();

    // Clear existing data
    context.WeatherConditions.RemoveRange(context.WeatherConditions);
    context.ClothingItems.RemoveRange(context.ClothingItems);
    context.SaveChanges();

    // Seed weather conditions
    var weatherConditions = new List<WeatherCondition>
    {
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
    };

    context.WeatherConditions.AddRange(weatherConditions);
    context.SaveChanges();

    // Seed clothing items with appropriate weather condition associations
    var clothingItems = new List<ClothingItem>
    {
        // Arctic & Extreme Cold (-40 to -20)
        new ClothingItem
        {
            Name = "Heavy Winter Coat",
            Description = "Insulated coat for extreme cold conditions",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Arctic" or "Extreme Cold").ToList()
        },
        new ClothingItem
        {
            Name = "Thermal Base Layer",
            Description = "Moisture-wicking thermal underwear for extreme cold",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Arctic" or "Extreme Cold" or "Very Cold").ToList()
        },
        new ClothingItem
        {
            Name = "Insulated Snow Pants",
            Description = "Waterproof and insulated pants for extreme cold",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Arctic" or "Extreme Cold").ToList()
        },
        new ClothingItem
        {
            Name = "Winter Boots",
            Description = "Insulated waterproof boots for snow and extreme cold",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Arctic" or "Extreme Cold" or "Very Cold").ToList()
        },

        // Very Cold & Cold (-20 to 0)
        new ClothingItem
        {
            Name = "Winter Jacket",
            Description = "Warm jacket for cold weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Very Cold" or "Cold").ToList()
        },
        new ClothingItem
        {
            Name = "Wool Sweater",
            Description = "Warm wool sweater for layering in cold weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Very Cold" or "Cold" or "Chilly").ToList()
        },
        new ClothingItem
        {
            Name = "Winter Accessories Set",
            Description = "Includes warm hat, gloves, and scarf",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Very Cold" or "Cold").ToList()
        },

        // Chilly & Cool (0 to 15)
        new ClothingItem
        {
            Name = "Light Jacket",
            Description = "Light jacket for cool weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Chilly" or "Cool").ToList()
        },
        new ClothingItem
        {
            Name = "Long Sleeve Shirt",
            Description = "Comfortable for cool temperatures",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Chilly" or "Cool" or "Mild").ToList()
        },
        new ClothingItem
        {
            Name = "Light Sweater",
            Description = "Perfect for layering in cool weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Chilly" or "Cool").ToList()
        },

        // Mild (15 to 20)
        new ClothingItem
        {
            Name = "Light Long Sleeve T-Shirt",
            Description = "Comfortable for mild temperatures",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Mild").ToList()
        },
        new ClothingItem
        {
            Name = "Light Cotton Pants",
            Description = "Breathable pants for mild weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Mild" or "Cool").ToList()
        },
        new ClothingItem
        {
            Name = "Light Cardigan",
            Description = "Perfect for mild temperatures with slight breeze",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Mild").ToList()
        },

        // Warm (20 to 25)
        new ClothingItem
        {
            Name = "Short Sleeve T-Shirt",
            Description = "Perfect for warm weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Warm").ToList()
        },
        new ClothingItem
        {
            Name = "Light Cotton Shorts",
            Description = "Breathable shorts for warm weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Warm" or "Hot").ToList()
        },
        new ClothingItem
        {
            Name = "Light Cotton Dress",
            Description = "Comfortable dress for warm weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Warm" or "Hot").ToList()
        },

        // Hot & Very Hot (25 to 40)
        new ClothingItem
        {
            Name = "Breathable Tank Top",
            Description = "Lightweight and airy for hot weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Hot" or "Very Hot").ToList()
        },
        new ClothingItem
        {
            Name = "Quick-Dry Shorts",
            Description = "Moisture-wicking shorts for hot weather",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Hot" or "Very Hot").ToList()
        },
        new ClothingItem
        {
            Name = "Sun Protection Hat",
            Description = "Wide-brimmed hat for sun protection",
            WeatherConditions = weatherConditions.Where(w => w.ConditionName is "Hot" or "Very Hot").ToList()
        }
    };

    context.ClothingItems.AddRange(clothingItems);
    context.SaveChanges();
}

app.Run();
