using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class EditedPortions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "kmlength",
                table: "Portion",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "previous",
                table: "Portion",
                type: "int",
                nullable: true);

            /*migrationBuilder.CreateTable(
                name: "Etat",
                columns: table => new
                {
                    routeid = table.Column<string>(type: "varchar(20)", nullable: true),
                    name = table.Column<string>(type: "varchar(20)", nullable: true),
                    depart = table.Column<string>(type: "varchar(20)", nullable: true),
                    arrive = table.Column<string>(type: "varchar(20)", nullable: true),
                    etatglobal = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_Portion_previous",
                table: "Portion",
                column: "previous");

            migrationBuilder.AddForeignKey(
                name: "FK_Portion_Portion_previous",
                table: "Portion",
                column: "previous",
                principalTable: "Portion",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portion_Portion_previous",
                table: "Portion");

/*            migrationBuilder.DropTable(
                name: "Etat");*/

            migrationBuilder.DropIndex(
                name: "IX_Portion_previous",
                table: "Portion");

            migrationBuilder.DropColumn(
                name: "kmlength",
                table: "Portion");

            migrationBuilder.DropColumn(
                name: "previous",
                table: "Portion");
        }
    }
}
