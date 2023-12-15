using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class add_NotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MessageType = table.Column<string>(type: "varchar(20)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd957404-d019-404a-86cb-beaf4259409f", new DateTime(2023, 12, 15, 12, 15, 34, 694, DateTimeKind.Local).AddTicks(9634), "AQAAAAIAAYagAAAAEKcwFODqY+B7MHEGpn29LsUQAYq1aDDYNQJucPUsIGNfEmElYubWETqrHEIEibA6Pg==", "1dcbf3e2-0034-4f23-b05b-3df303d5e17e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "474384ae-a8d7-4802-bd7d-e495ffb6f255", new DateTime(2023, 12, 8, 15, 23, 46, 741, DateTimeKind.Local).AddTicks(6802), "AQAAAAIAAYagAAAAEKKQIwU4gCkJL3HU65jsBpxuUQqnCNkb0N4QIwHTTR902Bf+HwIqWXST7rZ2P9Y/YQ==", "8d718487-96e3-4c30-928b-14c62cd09d12" });
        }
    }
}
