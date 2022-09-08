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
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HostName = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    OSName = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Hosts_HostName",
                        column: x => x.HostName,
                        principalTable: "Hosts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_OSs_OSName",
                        column: x => x.OSName,
                        principalTable: "OSs",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HostName",
                table: "Reservations",
                column: "HostName");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OSName",
                table: "Reservations",
                column: "OSName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "OSs");
        }
    }
}
