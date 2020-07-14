using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class AddDataToRegions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionID", "DateOfEstablishment", "RegionDescription" },
                values: new object[] { 0, new DateTime(2020, 7, 10, 6, 31, 9, 959, DateTimeKind.Utc).AddTicks(1515), "region1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0);
        }
    }
}
