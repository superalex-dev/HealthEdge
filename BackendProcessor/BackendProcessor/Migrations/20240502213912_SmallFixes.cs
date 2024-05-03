using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class SmallFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurance_Doctors_DoctorId",
                table: "DoctorInsurance");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurance_Insurance_InsuranceId",
                table: "DoctorInsurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance");

            migrationBuilder.RenameTable(
                name: "DoctorInsurance",
                newName: "DoctorInsurances");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorInsurance_InsuranceId",
                table: "DoctorInsurances",
                newName: "IX_DoctorInsurances_InsuranceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorInsurances",
                table: "DoctorInsurances",
                columns: new[] { "DoctorId", "InsuranceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurances_Doctors_DoctorId",
                table: "DoctorInsurances",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurances_Insurance_InsuranceId",
                table: "DoctorInsurances",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurances_Doctors_DoctorId",
                table: "DoctorInsurances");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorInsurances_Insurance_InsuranceId",
                table: "DoctorInsurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorInsurances",
                table: "DoctorInsurances");

            migrationBuilder.RenameTable(
                name: "DoctorInsurances",
                newName: "DoctorInsurance");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorInsurances_InsuranceId",
                table: "DoctorInsurance",
                newName: "IX_DoctorInsurance_InsuranceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorInsurance",
                table: "DoctorInsurance",
                columns: new[] { "DoctorId", "InsuranceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurance_Doctors_DoctorId",
                table: "DoctorInsurance",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorInsurance_Insurance_InsuranceId",
                table: "DoctorInsurance",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
