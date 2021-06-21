using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLogic.Migrations
{
    public partial class EtatGloabalView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW Etat AS 
                SELECT CONCAT(route_id, ' ') routeid, name, depart, arrive, 
                    CASE 
                        WHEN smlg = 0 THEN '--' 
                        ELSE CONCAT((etat_gloabal / smlg), ' ') 
                    END AS etatglobal 
                FROM v_data_sum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view Etat");
        }
    }
}
