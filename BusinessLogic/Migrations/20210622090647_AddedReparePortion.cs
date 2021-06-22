using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddedReparePortion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Repareportion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_rep = table.Column<DateTime>(type: "datetime", nullable: true),
                    portion_id = table.Column<int>(type: "int", nullable: false),
                    durree_reparation = table.Column<decimal>(type: "DECIMAL(19,5)", nullable: false),
                    prix_reparation = table.Column<decimal>(type: "DECIMAL(19,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repareportion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Repareportion_Portion_portion_id",
                        column: x => x.portion_id,
                        principalTable: "Portion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repareportion_portion_id",
                table: "Repareportion",
                column: "portion_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repareportion");
        }
    }
}
