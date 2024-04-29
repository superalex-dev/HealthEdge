using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class UserPatientsRefactorLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Patients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Patients");
        }
    }
}
