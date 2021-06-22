using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddPrixDureeRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "durree_reparation",
                table: "Reparation",
                type: "DECIMAL(19,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "prix_reparation",
                table: "Reparation",
                type: "DECIMAL(19,5)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "durree_reparation",
                table: "Reparation");

            migrationBuilder.DropColumn(
                name: "prix_reparation",
                table: "Reparation");
        }
    }
}
