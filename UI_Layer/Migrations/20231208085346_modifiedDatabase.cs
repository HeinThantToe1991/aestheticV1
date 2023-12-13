using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class modifiedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "ApplicationUser",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "474384ae-a8d7-4802-bd7d-e495ffb6f255", new DateTime(2023, 12, 8, 15, 23, 46, 741, DateTimeKind.Local).AddTicks(6802), "AQAAAAIAAYagAAAAEKKQIwU4gCkJL3HU65jsBpxuUQqnCNkb0N4QIwHTTR902Bf+HwIqWXST7rZ2P9Y/YQ==", "8d718487-96e3-4c30-928b-14c62cd09d12", "S" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ApplicationUser");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d6ac956-6116-4037-a4bb-8b516ef99845", new DateTime(2023, 12, 8, 11, 29, 14, 532, DateTimeKind.Local).AddTicks(3990), "AQAAAAIAAYagAAAAEI8SEKyTU7tplQ3v0NJeHUSFdoX4GnJFuf2Q/evULReazveTaMaSef4nIfqNI/KnQg==", "9c3b0af2-bbf5-4cd7-b711-13025ab819a4" });
        }
    }
}
