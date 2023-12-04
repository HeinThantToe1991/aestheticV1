using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI_Layer.Migrations
{
    /// <inheritdoc />
    public partial class add_companyInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66b95a27-4904-45fd-a3e9-1d12948d8dd3", new DateTime(2023, 8, 14, 15, 39, 14, 121, DateTimeKind.Local).AddTicks(4579), "AQAAAAIAAYagAAAAEH6Kx25JzCnz3/nkx1l/XOXOWXIrDo9lfIoxUDX3I9mxmMoMIvbx1c/DOiV0h/XsMQ==", "c4890da2-1da0-4dbd-98ed-12b99f9a4740" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInformation");

            migrationBuilder.UpdateData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("8d7fae5e-3cd5-497b-b334-86790609eedc"),
                columns: new[] { "ConcurrencyStamp", "CreatedDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07aa12ca-597f-4dcc-943c-b891b98a8b4b", new DateTime(2023, 8, 2, 0, 51, 29, 392, DateTimeKind.Local).AddTicks(4056), "AQAAAAIAAYagAAAAEHccCqWDCMAtAatryPWFIjc7Vg8iCHYahr23SLS6caejNqIfxUWbHJRPVceAxpqeRQ==", "d9104888-9822-4888-8ebc-5d11aa0065bd" });
        }
    }
}
