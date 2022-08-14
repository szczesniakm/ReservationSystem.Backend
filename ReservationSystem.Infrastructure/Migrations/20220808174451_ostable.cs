using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Infrastructure.Migrations
{
    public partial class ostable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_OS_OSName",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OS",
                table: "OS");

            migrationBuilder.RenameTable(
                name: "OS",
                newName: "OSs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OSs",
                table: "OSs",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_OSs_OSName",
                table: "Reservations",
                column: "OSName",
                principalTable: "OSs",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_OSs_OSName",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OSs",
                table: "OSs");

            migrationBuilder.RenameTable(
                name: "OSs",
                newName: "OS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OS",
                table: "OS",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_OS_OSName",
                table: "Reservations",
                column: "OSName",
                principalTable: "OS",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
