using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationSystem.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "OSs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSs", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ReseservationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Host = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    OS = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReseservationLogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s1", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s2", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s3", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s4", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s5", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s6", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s7", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s8", "" });

            migrationBuilder.InsertData(
                table: "Hosts",
                columns: new[] { "Name", "Status" },
                values: new object[] { "s9", "" });

            migrationBuilder.InsertData(
                table: "OSs",
                column: "Name",
                value: "archlinux console");

            migrationBuilder.InsertData(
                table: "OSs",
                column: "Name",
                value: "windows 10");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "OSs");

            migrationBuilder.DropTable(
                name: "ReseservationLogs");
        }
    }
}
