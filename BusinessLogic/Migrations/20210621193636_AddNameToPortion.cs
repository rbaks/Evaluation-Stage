using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddNameToPortion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Portion",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portion_name",
                table: "Portion",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Portion_name",
                table: "Portion");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Portion");
        }
    }
}
