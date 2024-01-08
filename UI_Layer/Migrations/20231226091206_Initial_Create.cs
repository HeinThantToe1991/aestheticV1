using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "char(36)", nullable: false),
                    UserType = table.Column<string>(type: "char(1)", nullable: false),
                    ConnectionId = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(25)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemUser = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<string>(type: "char(1)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_ApplicationUser_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "CompanyInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    RefCompanyId = table.Column<string>(type: "varchar(100)", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CompanyShortName = table.Column<string>(type: "varchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(50)", nullable: false),
                    CompanyLogo = table.Column<string>(type: "varchar(50)", nullable: false),
                    BusinessType = table.Column<string>(type: "varchar(200)", nullable: false),
                    StateDivision = table.Column<string>(type: "varchar(15)", nullable: false),
                    District = table.Column<string>(type: "varchar(15)", nullable: false),
                    Township = table.Column<string>(type: "varchar(15)", nullable: false),
                    StreetNumber_Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "varchar(254)", nullable: false),
                    FacebookLink = table.Column<string>(type: "varchar(200)", nullable: false),
                    WebsiteLink = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SystemDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInformation", x => x.Id);
                });

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
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
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
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "ApplicationRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3b79e7b8-9a4a-41be-883a-5853056716d7"), null, "Staff Role", "Staff", "STAFF" },
                    { new Guid("3b79e7b8-9a4a-41be-883a-5853056716e9"), null, "Customer Role", "Customer", "Customer" },
                    { new Guid("d9cbb087-b31f-4441-8506-c21c7e0c15a4"), null, "Admin Role", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "CreatedDate", "CreatedUserId", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SystemUser", "TwoFactorEnabled", "UpdatedDate", "UpdatedUserId", "UserName", "UserType" },
                values: new object[] { new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), 0, true, "5f983613-2fe8-42a9-bd89-ae34528f863d", new DateTime(2023, 12, 26, 15, 42, 5, 925, DateTimeKind.Local).AddTicks(4341), new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), "admin@gmail.com", true, "Admin", false, null, "admin@gmail.com", "ADMIN", "AQAAAAIAAYagAAAAEBap4wVfJPaamvBuwdfU8v2hhmUoImTUQChsYGZMGsbRZWosDEtZrowaBpbOcdoZwQ==", "1234567890", false, "1da7c63b-adcd-4c6f-8bd8-bed4a5831741", true, false, null, new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), "admin", "S" });

            migrationBuilder.InsertData(
                table: "ApplicationUserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d9cbb087-b31f-4441-8506-c21c7e0c15a4"), new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc") });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Active", "Address", "ApplicationUserId", "CreatedDate", "CreatedUserId", "DateOfBirth", "DeleteRemark", "Gender", "Remark", "SystemDefault", "TownshipId", "UpdatedDate", "UpdatedUserId", "UserImage" },
                values: new object[] { "3b79e7b8-9a4a-41be-883a-5853056716d8", true, "Addresstrue", new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), new DateTime(2023, 12, 26, 15, 42, 5, 980, DateTimeKind.Local).AddTicks(796), new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), new DateTime(1991, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Male", "Default Admin", true, "", null, new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"), "" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ApplicationRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_CreatedUserId",
                table: "ApplicationUser",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApplicationUserId",
                table: "Customer",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_ApplicationUserId",
                table: "Doctor",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ApplicationUserId",
                table: "Staff",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveUsers");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CompanyInformation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "ServiceStock");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "ApplicationRole");

            migrationBuilder.DropTable(
                name: "ApplicationUser");
        }
    }
}
