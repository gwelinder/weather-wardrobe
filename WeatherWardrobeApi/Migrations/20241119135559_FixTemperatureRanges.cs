using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherWardrobeApi.Migrations
{
    /// <inheritdoc />
    public partial class FixTemperatureRanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                column: "TemperatureRange",
                value: "30-40");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                column: "TemperatureRange",
                value: "30-45");
        }
    }
}
