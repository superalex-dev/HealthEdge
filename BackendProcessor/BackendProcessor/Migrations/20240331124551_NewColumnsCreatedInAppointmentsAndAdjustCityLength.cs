using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsCreatedInAppointmentsAndAdjustCityLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Doctors",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Doctors",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);
        }
    }
}
