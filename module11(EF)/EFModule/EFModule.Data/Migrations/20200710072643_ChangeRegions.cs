using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class ChangeRegions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 909, DateTimeKind.Utc).AddTicks(3971));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 910, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 910, DateTimeKind.Utc).AddTicks(4658));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 6, 54, 6, 19, DateTimeKind.Utc).AddTicks(1364));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 6, 54, 6, 20, DateTimeKind.Utc).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 6, 54, 6, 20, DateTimeKind.Utc).AddTicks(1726));
        }
    }
}
