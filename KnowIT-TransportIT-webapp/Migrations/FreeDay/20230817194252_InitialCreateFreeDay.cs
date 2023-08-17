using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowIT_TransportIT_webapp.Migrations.FreeDay
{
    public partial class InitialCreateFreeDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FreeDayClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FreeDayReason = table.Column<string>(type: "TEXT", nullable: false),
                    StatusFreeDay = table.Column<bool>(type: "INTEGER", nullable: false),
                    StartDateFreeDay = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDateFreeDay = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeDayClass", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreeDayClass");
        }
    }
}
