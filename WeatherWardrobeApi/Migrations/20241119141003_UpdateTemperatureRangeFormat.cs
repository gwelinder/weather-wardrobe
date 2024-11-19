using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherWardrobeApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemperatureRangeFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 1,
                column: "TemperatureRange",
                value: "-40 to -30");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 2,
                column: "TemperatureRange",
                value: "-30 to -20");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 3,
                column: "TemperatureRange",
                value: "-20 to -10");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 4,
                column: "TemperatureRange",
                value: "-10 to 0");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 5,
                column: "TemperatureRange",
                value: "0 to 10");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 6,
                column: "TemperatureRange",
                value: "10 to 15");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                column: "TemperatureRange",
                value: "15 to 20");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 8,
                column: "TemperatureRange",
                value: "20 to 25");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 9,
                column: "TemperatureRange",
                value: "25 to 30");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 10,
                column: "TemperatureRange",
                value: "30 to 40");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 1,
                column: "TemperatureRange",
                value: "-40--30");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 2,
                column: "TemperatureRange",
                value: "-30--20");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 3,
                column: "TemperatureRange",
                value: "-20--10");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 4,
                column: "TemperatureRange",
                value: "-10-0");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 5,
                column: "TemperatureRange",
                value: "0-10");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 6,
                column: "TemperatureRange",
                value: "10-15");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 7,
                column: "TemperatureRange",
                value: "15-20");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 8,
                column: "TemperatureRange",
                value: "20-25");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 9,
                column: "TemperatureRange",
                value: "25-30");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "WeatherConditionId",
                keyValue: 10,
                column: "TemperatureRange",
                value: "30-40");
        }
    }
}
