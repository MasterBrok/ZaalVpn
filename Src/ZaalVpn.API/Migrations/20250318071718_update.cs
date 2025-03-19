using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZaalVpn.API.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "1cbd9eb4cf61422cbdfdde046471ef6b");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "5f05feca029045e2827109cd8720e18e");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "670b3bf1ceb94eb992a85c199d3aeabe");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "831900da22ed420a85fcc7a50a463473");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "a3c58dc9b2184bf89041ea3a870eaf4d");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "a93641c818ea41bc96ca07318d5a7204");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "c52dc6e878ec41d989ffdaa1a74e310a");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e0fdd41-e6ce-48f5-b614-30b3fdaccc4f", "ebae9dc1-6b62-407e-8a70-ab8da1e39257" });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9070aa08-14de-4ca1-9d83-ceb6f9be6a27", "ebae9dc1-6b62-407e-8a70-ab8da1e39257" });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbRoles",
                keyColumn: "Id",
                keyValue: "2e0fdd41-e6ce-48f5-b614-30b3fdaccc4f");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbRoles",
                keyColumn: "Id",
                keyValue: "9070aa08-14de-4ca1-9d83-ceb6f9be6a27");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUsers",
                keyColumn: "Id",
                keyValue: "ebae9dc1-6b62-407e-8a70-ab8da1e39257");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "1ad5a945d7904c609b5c12ffecd69c3b");

            migrationBuilder.DropColumn(
                name: "LastOnlineAt",
                schema: "auth",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "auth",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "auth",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "auth",
                table: "tbUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastOnlineAt",
                schema: "auth",
                table: "tbUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "auth",
                table: "tbUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "auth",
                table: "tbUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "auth",
                table: "tbUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbGenders",
                columns: new[] { "Id", "CreateTimeAt", "Gender" },
                values: new object[,]
                {
                    { "1ad5a945d7904c609b5c12ffecd69c3b", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8596), "Male" },
                    { "1cbd9eb4cf61422cbdfdde046471ef6b", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8659), "Female" },
                    { "5f05feca029045e2827109cd8720e18e", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8700), "Agender" },
                    { "670b3bf1ceb94eb992a85c199d3aeabe", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8748), "Androgynous" },
                    { "831900da22ed420a85fcc7a50a463473", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8723), "Feminine" },
                    { "a3c58dc9b2184bf89041ea3a870eaf4d", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8760), "Other" },
                    { "a93641c818ea41bc96ca07318d5a7204", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8688), "Bigender" },
                    { "c52dc6e878ec41d989ffdaa1a74e310a", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(8676), "NonBinary" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e0fdd41-e6ce-48f5-b614-30b3fdaccc4f", null, "Admin", null },
                    { "9070aa08-14de-4ca1-9d83-ceb6f9be6a27", null, "User", null }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationTimeAt", "Email", "EmailConfirmed", "GenderId", "LastOnlineAt", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShortId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ebae9dc1-6b62-407e-8a70-ab8da1e39257", 0, "3631fded-e45b-4576-84a4-470f9fee60c3", new DateTime(2025, 3, 15, 14, 13, 17, 849, DateTimeKind.Local).AddTicks(9917), "brok@gmail.com", true, "1ad5a945d7904c609b5c12ffecd69c3b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, "AQAAAAIAAYagAAAAEMKrGYCR2qyGSZAXCEvmdz8vhdq5SqGPuX3tghdYF7vbjoeI1o35Koc/0waf1SFEOQ==", null, true, "e05f6f3f-3073-4dc8-81f6-4258a3f8662e", "111111", false, "brok" });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2e0fdd41-e6ce-48f5-b614-30b3fdaccc4f", "ebae9dc1-6b62-407e-8a70-ab8da1e39257" },
                    { "9070aa08-14de-4ca1-9d83-ceb6f9be6a27", "ebae9dc1-6b62-407e-8a70-ab8da1e39257" }
                });
        }
    }
}
