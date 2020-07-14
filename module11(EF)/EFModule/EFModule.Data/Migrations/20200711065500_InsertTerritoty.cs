using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class InsertTerritoty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Territories",
                columns: new[] { "TerritoryID", "RegionID", "TerritoryDescription" },
                values: new object[] { "0", 0, "Terr description" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "TerritoryID",
                keyValue: "0");
        }
    }
}
