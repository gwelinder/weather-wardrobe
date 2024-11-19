using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherWardrobeApi.Migrations
{
    /// <inheritdoc />
    public partial class ExtendTemperatureRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.InsertData(
                table: "ClothingItemWeatherCondition",
                columns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 3, 7 },
                    { 4, 5 },
                    { 4, 6 },
                    { 6, 5 },
                    { 6, 6 },
                    { 6, 7 },
                    { 7, 3 },
                    { 7, 4 },
                    { 9, 3 },
                    { 9, 4 }
                });

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "ClothingItemId", "Description", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 11, "Base layer thermal underwear", "thermal.jpg", "Thermal Underwear" },
                    { 12, "Insulated snow pants", "snowpants.jpg", "Snow Pants" },
                    { 13, "Extreme cold weather parka", "parka.jpg", "Arctic Parka" },
                    { 14, "Thermal neck protection", "gaiter.jpg", "Neck Gaiter" },
                    { 15, "Heavy-duty winter gloves", "gloves.jpg", "Insulated Gloves" }
                });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 1,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Arctic", "-40--30" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 2,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Extreme Cold", "-30--20" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 3,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Very Cold", "-20--10" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 4,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Cold", "-10-0" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 5,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Chilly", "0-10" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 6,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Cool", "10-15" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Mild", "15-20" });

            migrationBuilder.InsertData(
                table: "WeatherConditions",
                columns: new[] { "WeatherConditionId", "ConditionName", "Temperature", "TemperatureRange" },
                values: new object[,]
                {
                    { 8, "Warm", 0.0, "20-25" },
                    { 9, "Hot", 0.0, "25-30" },
                    { 10, "Very Hot", 0.0, "30-40" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemWeatherCondition",
                columns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                values: new object[,]
                {
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 8, 8 },
                    { 8, 9 },
                    { 8, 10 },
                    { 10, 9 },
                    { 10, 10 },
                    { 11, 1 },
                    { 11, 2 },
                    { 11, 3 },
                    { 12, 1 },
                    { 12, 2 },
                    { 12, 3 },
                    { 12, 4 },
                    { 13, 1 },
                    { 13, 2 },
                    { 14, 1 },
                    { 14, 2 },
                    { 14, 3 },
                    { 15, 1 },
                    { 15, 2 },
                    { 15, 3 },
                    { 15, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 8, 10 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 15, 3 });

            migrationBuilder.DeleteData(
                table: "ClothingItemWeatherCondition",
                keyColumns: new[] { "ClothingItemsClothingItemId", "WeatherConditionsWeatherConditionId" },
                keyValues: new object[] { 15, 4 });

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "ClothingItemId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 10);

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
                    { 8, 5 },
                    { 8, 6 },
                    { 8, 7 },
                    { 10, 6 },
                    { 10, 7 }
                });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 1,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Freezing", "-20-0" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 2,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Cold", "0-10" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 3,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Cool", "10-15" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 4,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Mild", "15-20" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 5,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Warm", "20-25" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 6,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Hot", "25-30" });

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                columns: new[] { "ConditionName", "TemperatureRange" },
                values: new object[] { "Scorching", "30-40" });
        }
    }
}
