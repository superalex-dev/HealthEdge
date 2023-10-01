using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProcessor.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseContactNumberLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientFirstName",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Billing_Patients_PatientFirstName",
                table: "Billing");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientFirstName",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_VIPRooms_Rooms_RoomNumber",
                table: "VIPRooms");

            migrationBuilder.DropIndex(
                name: "IX_VIPRooms_RoomNumber",
                table: "VIPRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PatientFirstName",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Billing_PatientFirstName",
                table: "Billing");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientFirstName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "VIPRooms");

            migrationBuilder.DropColumn(
                name: "PatientFirstName",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "PatientFirstName",
                table: "Billing");

            migrationBuilder.DropColumn(
                name: "PatientFirstName",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Patients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_VIPRooms_RoomId",
                table: "VIPRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_PatientId",
                table: "Billing",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_Patients_PatientId",
                table: "Billing",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientId",
                table: "MedicalRecords",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VIPRooms_Rooms_RoomId",
                table: "VIPRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Billing_Patients_PatientId",
                table: "Billing");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientId",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_VIPRooms_Rooms_RoomId",
                table: "VIPRooms");

            migrationBuilder.DropIndex(
                name: "IX_VIPRooms_RoomId",
                table: "VIPRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Billing_PatientId",
                table: "Billing");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "VIPRooms",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Patients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "MedicalRecords",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "Billing",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientFirstName",
                table: "Appointments",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_VIPRooms_RoomNumber",
                table: "VIPRooms",
                column: "RoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientFirstName",
                table: "MedicalRecords",
                column: "PatientFirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_PatientFirstName",
                table: "Billing",
                column: "PatientFirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientFirstName",
                table: "Appointments",
                column: "PatientFirstName");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientFirstName",
                table: "Appointments",
                column: "PatientFirstName",
                principalTable: "Patients",
                principalColumn: "FirstName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_Patients_PatientFirstName",
                table: "Billing",
                column: "PatientFirstName",
                principalTable: "Patients",
                principalColumn: "FirstName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientFirstName",
                table: "MedicalRecords",
                column: "PatientFirstName",
                principalTable: "Patients",
                principalColumn: "FirstName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VIPRooms_Rooms_RoomNumber",
                table: "VIPRooms",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
