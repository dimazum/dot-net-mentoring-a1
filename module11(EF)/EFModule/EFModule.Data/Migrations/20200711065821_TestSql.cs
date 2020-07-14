using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class TestSql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE [dbo].[Territories] SET [TerritoryDescription] = 'TerrTest2' WHERE [TerritoryID] = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE [dbo].[Territories] SET [TerritoryDescription] = 'Terr description' WHERE [TerritoryID] = 0");
        }
    }
}
