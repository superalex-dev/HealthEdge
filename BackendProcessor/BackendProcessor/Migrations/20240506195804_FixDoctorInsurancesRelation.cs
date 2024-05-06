using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctorInsurancesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Insurance_InsuranceId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_InsuranceId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "Doctors",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_InsuranceId",
                table: "Doctors",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Insurance_InsuranceId",
                table: "Doctors",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
