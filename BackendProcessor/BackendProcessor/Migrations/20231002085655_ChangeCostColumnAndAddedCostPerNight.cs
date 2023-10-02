using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCostColumnAndAddedCostPerNight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "RoomCosts");

            migrationBuilder.DropColumn(
                name: "WeeklyRate",
                table: "RoomCosts");

            migrationBuilder.AddColumn<float>(
                name: "CostPerNight",
                table: "RoomCosts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPerNight",
                table: "RoomCosts");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyRate",
                table: "RoomCosts",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WeeklyRate",
                table: "RoomCosts",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
