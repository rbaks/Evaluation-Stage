using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddEtatRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Portion_name",
                table: "Portion");

            migrationBuilder.AddColumn<string>(
                name: "etat",
                table: "Route",
                type: "varchar(20)",
                defaultValue: "Modifiable",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Portion",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portion_name",
                table: "Portion",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Portion_name",
                table: "Portion");

            migrationBuilder.DropColumn(
                name: "etat",
                table: "Route");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Portion",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.CreateIndex(
                name: "IX_Portion_name",
                table: "Portion",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");
        }
    }
}
