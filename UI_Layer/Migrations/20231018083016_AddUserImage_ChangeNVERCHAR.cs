using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImage_ChangeNVERCHAR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Customer",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "Customer",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cea4430-d2a4-4912-8df6-ce5157734916", new DateTime(2023, 10, 18, 15, 0, 15, 873, DateTimeKind.Local).AddTicks(1924), "AQAAAAIAAYagAAAAEPNKNrcspiPQ/QxdFA67kkGGZzxQBdrd6qAknxz+MtobK63FhoqzfvS9Q+HDnwAF0Q==", "d6ed5193-5221-4b93-acfc-1d690d085c00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Customer",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff7dd68a-0e41-41b4-bb2e-cbe857745619", new DateTime(2023, 9, 8, 11, 31, 46, 138, DateTimeKind.Local).AddTicks(3999), "AQAAAAIAAYagAAAAEKXFHlp8yWAvis4wi6ha9BLxlTARx6D2rEf8nyS0E5Vyz5lp4kggqcQd466NKU728w==", "82eb6769-087b-45dd-ab1d-d1dd561be68d" });
        }
    }
}
