using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TownshipId = table.Column<string>(type: "varchar(15)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DeleteRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d6ac956-6116-4037-a4bb-8b516ef99845", new DateTime(2023, 12, 8, 11, 29, 14, 532, DateTimeKind.Local).AddTicks(3990), "AQAAAAIAAYagAAAAEI8SEKyTU7tplQ3v0NJeHUSFdoX4GnJFuf2Q/evULReazveTaMaSef4nIfqNI/KnQg==", "9c3b0af2-bbf5-4cd7-b711-13025ab819a4" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ApplicationUserId",
                table: "Staff",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc251b40-e458-45b1-abea-8a5f41a0bdb2", new DateTime(2023, 10, 18, 16, 27, 3, 73, DateTimeKind.Local).AddTicks(3984), "AQAAAAIAAYagAAAAEKyp6u9v2s4Kw7g7YbiuPvPV0bQxEt0yqVBLxtwNRweGxLAKRAuQptQzTlgtWN7oLA==", "e86b5122-a39b-4f11-8d12-8b83a32f768a" });
        }
    }
}
