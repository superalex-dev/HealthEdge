using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class addedSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Doctors",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPediatrician",
                table: "Doctors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsPediatrician",
                table: "Doctors");
        }
    }
}
