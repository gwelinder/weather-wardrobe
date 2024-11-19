using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherWardrobeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingItems_WeatherConditions_WeatherConditionId",
                table: "ClothingItems");

            migrationBuilder.DropIndex(
                name: "IX_ClothingItems_WeatherConditionId",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "WeatherConditionId",
                table: "ClothingItems");

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "WeatherConditions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ClothingItemWeatherCondition",
                columns: table => new
                {
                    ClothingItemsClothingItemId = table.Column<int>(type: "integer", nullable: false),
                    WeatherConditionsWeatherConditionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItemWeatherCondition", x => new { x.ClothingItemsClothingItemId, x.WeatherConditionsWeatherConditionId });
                    table.ForeignKey(
                        name: "FK_ClothingItemWeatherCondition_ClothingItems_ClothingItemsClo~",
                        column: x => x.ClothingItemsClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "ClothingItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingItemWeatherCondition_WeatherConditions_WeatherCondi~",
                        column: x => x.WeatherConditionsWeatherConditionId,
                        principalTable: "WeatherConditions",
                        principalColumn: "WeatherConditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WeatherConditions",
                columns: new[] { "WeatherConditionId", "ConditionName", "Temperature", "TemperatureRange" },
                values: new object[,]
                {
                    { 1, "Freezing", 0.0, "-20-0" },
                    { 2, "Cold", 0.0, "0-10" },
                    { 3, "Cool", 0.0, "10-15" },
                    { 4, "Mild", 0.0, "15-20" },
                    { 5, "Warm", 0.0, "20-25" },
                    { 6, "Hot", 0.0, "25-30" },
                    { 7, "Scorching", 0.0, "30-45" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItemWeatherCondition_WeatherConditionsWeatherCondit~",
                table: "ClothingItemWeatherCondition",
                column: "WeatherConditionsWeatherConditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingItemWeatherCondition");

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "WeatherConditions");

            migrationBuilder.AddColumn<int>(
                name: "WeatherConditionId",
                table: "ClothingItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItems_WeatherConditionId",
                table: "ClothingItems",
                column: "WeatherConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingItems_WeatherConditions_WeatherConditionId",
                table: "ClothingItems",
                column: "WeatherConditionId",
                principalTable: "WeatherConditions",
                principalColumn: "WeatherConditionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
