using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class InsertDataToRegoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 6, 54, 6, 19, DateTimeKind.Utc).AddTicks(1364));

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionID", "DateOfEstablishment", "RegionDescription" },
                values: new object[] { 2, new DateTime(2020, 7, 10, 6, 54, 6, 20, DateTimeKind.Utc).AddTicks(1726), "region3" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionID", "DateOfEstablishment", "RegionDescription" },
                values: new object[] { 1, new DateTime(2020, 7, 10, 6, 54, 6, 20, DateTimeKind.Utc).AddTicks(1702), "region2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 6, 31, 9, 959, DateTimeKind.Utc).AddTicks(1515));
        }
    }
}
