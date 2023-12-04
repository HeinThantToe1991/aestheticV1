using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addCategory_ServiceStock_Doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
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
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceStock",
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
                    table.PrimaryKey("PK_ServiceStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TownshipId = table.Column<string>(type: "varchar(15)", nullable: false),
                    CategoryId = table.Column<string>(type: "char(36)", nullable: false),
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
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc251b40-e458-45b1-abea-8a5f41a0bdb2", new DateTime(2023, 10, 18, 16, 27, 3, 73, DateTimeKind.Local).AddTicks(3984), "AQAAAAIAAYagAAAAEKyp6u9v2s4Kw7g7YbiuPvPV0bQxEt0yqVBLxtwNRweGxLAKRAuQptQzTlgtWN7oLA==", "e86b5122-a39b-4f11-8d12-8b83a32f768a" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_ApplicationUserId",
                table: "Doctor",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_CategoryId",
                table: "Doctor",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "ServiceStock");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cea4430-d2a4-4912-8df6-ce5157734916", new DateTime(2023, 10, 18, 15, 0, 15, 873, DateTimeKind.Local).AddTicks(1924), "AQAAAAIAAYagAAAAEPNKNrcspiPQ/QxdFA67kkGGZzxQBdrd6qAknxz+MtobK63FhoqzfvS9Q+HDnwAF0Q==", "d6ed5193-5221-4b93-acfc-1d690d085c00" });
        }
    }
}
