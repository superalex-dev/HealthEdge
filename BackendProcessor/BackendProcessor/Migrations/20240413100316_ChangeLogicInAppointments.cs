using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLogicInAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Treatment",
                table: "Appointments",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "RecordDate",
                table: "Appointments",
                newName: "AppointmentTime");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Appointments",
                newName: "Treatment");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Appointments",
                newName: "RecordDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Appointments",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

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
    }
}
