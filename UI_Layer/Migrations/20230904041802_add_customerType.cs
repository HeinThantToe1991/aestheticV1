using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class add_customerType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91db6129-ea0f-42dd-ad4d-a53e821953ef", new DateTime(2023, 9, 4, 10, 48, 2, 389, DateTimeKind.Local).AddTicks(8333), "AQAAAAIAAYagAAAAEF9cjhhCL9qayYweRkGgCIwiXLSOZT5cYwBq5/6CcSpRglc6ctSvSox7ohvw1l41YQ==", "ece70be0-5b22-48c3-87ef-a60af4106a73" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66b95a27-4904-45fd-a3e9-1d12948d8dd3", new DateTime(2023, 8, 14, 15, 39, 14, 121, DateTimeKind.Local).AddTicks(4579), "AQAAAAIAAYagAAAAEH6Kx25JzCnz3/nkx1l/XOXOWXIrDo9lfIoxUDX3I9mxmMoMIvbx1c/DOiV0h/XsMQ==", "c4890da2-1da0-4dbd-98ed-12b99f9a4740" });
        }
    }
}
