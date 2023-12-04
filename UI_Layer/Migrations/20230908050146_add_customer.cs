using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class add_customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TownshipId = table.Column<string>(type: "varchar(15)", nullable: false),
                    CustomerTypeId = table.Column<string>(type: "char(36)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Remark = table.Column<string>(type: "varchar(250)", nullable: false),
                    DeleteRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff7dd68a-0e41-41b4-bb2e-cbe857745619", new DateTime(2023, 9, 8, 11, 31, 46, 138, DateTimeKind.Local).AddTicks(3999), "AQAAAAIAAYagAAAAEKXFHlp8yWAvis4wi6ha9BLxlTARx6D2rEf8nyS0E5Vyz5lp4kggqcQd466NKU728w==", "82eb6769-087b-45dd-ab1d-d1dd561be68d" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApplicationUserId",
                table: "Customer",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91db6129-ea0f-42dd-ad4d-a53e821953ef", new DateTime(2023, 9, 4, 10, 48, 2, 389, DateTimeKind.Local).AddTicks(8333), "AQAAAAIAAYagAAAAEF9cjhhCL9qayYweRkGgCIwiXLSOZT5cYwBq5/6CcSpRglc6ctSvSox7ohvw1l41YQ==", "ece70be0-5b22-48c3-87ef-a60af4106a73" });
        }
    }
}
