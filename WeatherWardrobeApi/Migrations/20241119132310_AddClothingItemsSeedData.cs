using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherWardrobeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddClothingItemsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "ClothingItemId", "Description", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, "Heavy insulated winter coat", "coat.jpg", "Winter Coat" },
                    { 2, "Light windbreaker jacket", "jacket.jpg", "Light Jacket" },
                    { 3, "Cotton t-shirt", "tshirt.jpg", "T-Shirt" },
                    { 4, "Warm wool sweater", "sweater.jpg", "Sweater" },
                    { 5, "Casual shorts", "shorts.jpg", "Shorts" },
                    { 6, "Regular fit jeans", "pants.jpg", "Long Pants" },
                    { 7, "Warm beanie", "hat.jpg", "Winter Hat" },
                    { 8, "Summer sandals", "sandals.jpg", "Sandals" },
                    { 9, "Insulated winter boots", "boots.jpg", "Winter Boots" },
                    { 10, "Light sleeveless top", "tank.jpg", "Tank Top" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemWeatherCondition",
                columns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 2 },
                    { 4, 3 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 4 },
                    { 7, 1 },
                    { 7, 2 },
                    { 8, 5 },
                    { 8, 6 },
                    { 8, 7 },
                    { 9, 1 },
                    { 9, 2 },
                    { 10, 6 },
                    { 10, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 10);
        }
    }
}
