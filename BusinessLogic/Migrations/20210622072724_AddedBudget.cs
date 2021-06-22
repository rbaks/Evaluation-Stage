using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddedBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entrees = table.Column<decimal>(type: "decimal(19,5)", nullable: false, defaultValue: 0),
                    date_entree = table.Column<DateTime>(type: "datetime", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget");
        }
    }
}
