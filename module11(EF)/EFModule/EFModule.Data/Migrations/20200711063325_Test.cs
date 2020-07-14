using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModule.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 0,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 910, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 910, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2,
                column: "DateOfEstablishment",
                value: new DateTime(2020, 7, 10, 7, 26, 42, 910, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
