using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class AddedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    perKmCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    perKmDuration = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_city = table.Column<int>(type: "int", nullable: false),
                    end_city = table.Column<int>(type: "int", nullable: false),
                    kmlength = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.id);
                    table.ForeignKey(
                        name: "FK_Route_City_end_city",
                        column: x => x.end_city,
                        principalTable: "City",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Route_City_start_city",
                        column: x => x.start_city,
                        principalTable: "City",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Portion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_portion = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    end_portion = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    state_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Portion_Route_route_id",
                        column: x => x.route_id,
                        principalTable: "Route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Portion_State_state_id",
                        column: x => x.state_id,
                        principalTable: "State",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portion_route_id",
                table: "Portion",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_Portion_state_id",
                table: "Portion",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_Route_end_city",
                table: "Route",
                column: "end_city");

            migrationBuilder.CreateIndex(
                name: "IX_Route_start_city",
                table: "Route",
                column: "start_city");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portion");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
