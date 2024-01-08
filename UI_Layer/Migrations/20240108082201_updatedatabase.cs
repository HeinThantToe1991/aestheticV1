using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyShortName",
                table: "CompanyInformation",
                type: "varchar(18)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e556090-2299-4768-8605-da339f9f6df9", new DateTime(2024, 1, 8, 14, 52, 0, 821, DateTimeKind.Local).AddTicks(9284), "AQAAAAIAAYagAAAAEIV9OiGWlw0BHO823DyInLTz1gP90MxbLKdSh9kHsVyDm+r/ze2dC3V/8yzGH/1/yw==", "870c3068-208c-44e0-ac2b-483b818fc4d2" });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: "3b79e7b8-9a4a-41be-883a-5853056716d8",
                column: "CreatedDate",
                value: new DateTime(2024, 1, 8, 14, 52, 0, 873, DateTimeKind.Local).AddTicks(9127));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyShortName",
                table: "CompanyInformation",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(18)");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f983613-2fe8-42a9-bd89-ae34528f863d", new DateTime(2023, 12, 26, 15, 42, 5, 925, DateTimeKind.Local).AddTicks(4341), "AQAAAAIAAYagAAAAEBap4wVfJPaamvBuwdfU8v2hhmUoImTUQChsYGZMGsbRZWosDEtZrowaBpbOcdoZwQ==", "1da7c63b-adcd-4c6f-8bd8-bed4a5831741" });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: "3b79e7b8-9a4a-41be-883a-5853056716d8",
                column: "CreatedDate",
                value: new DateTime(2023, 12, 26, 15, 42, 5, 980, DateTimeKind.Local).AddTicks(796));
        }
    }
}
