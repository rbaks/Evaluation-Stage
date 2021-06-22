using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddedPortionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "portion_id",
                table: "Reparation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reparation_portion_id",
                table: "Reparation",
                column: "portion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reparation_Portion_portion_id",
                table: "Reparation",
                column: "portion_id",
                principalTable: "Portion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparation_Portion_portion_id",
                table: "Reparation");

            migrationBuilder.DropIndex(
                name: "IX_Reparation_portion_id",
                table: "Reparation");

            migrationBuilder.DropColumn(
                name: "portion_id",
                table: "Reparation");
        }
    }
}
