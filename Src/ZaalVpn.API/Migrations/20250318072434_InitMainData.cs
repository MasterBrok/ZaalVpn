using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZaalVpn.API.Migrations
{
    /// <inheritdoc />
    public partial class InitMainData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbGenders",
                columns: new[] { "Id", "CreateTimeAt", "Gender" },
                values: new object[,]
                {
                    { "24da78b22d2f409d80285c0f6aa0000c", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(4051), "Androgynous" },
                    { "24f1c8d29ec843578f9d44c0ecfd037d", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(3899), "Female" },
                    { "5ad711dfe0284848bf67256c6fe24729", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(4000), "Bigender" },
                    { "70e096c446dd48d181854a3d42f07ef1", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(3982), "NonBinary" },
                    { "8322d0eabe3446e1adaa60e90bf1d9c8", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(3218), "Male" },
                    { "b03322517ab94376b543df4e4ba36e1f", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(4037), "Feminine" },
                    { "c297e2da90a74adbb15c2c050e715863", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(4014), "Agender" },
                    { "e37838f172f6449f880544f7ce97f60c", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(4063), "Other" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54c9bb4b-1693-4818-8a71-fb9b9c825a8c", null, "Admin", null },
                    { "559ecd36-deaf-4c9b-882a-598830d69103", null, "User", null }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationTimeAt", "Email", "EmailConfirmed", "GenderId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "ShortId", "UserName" },
                values: new object[] { "cb6b5c8e-e9b4-4956-8149-aba76a6552f7", 0, "d2c70f2b-6e8f-4db1-b429-af19369df6e4", new DateTime(2025, 3, 18, 10, 54, 30, 165, DateTimeKind.Local).AddTicks(5610), "brok@gmail.com", true, "8322d0eabe3446e1adaa60e90bf1d9c8", false, null, null, null, "AQAAAAIAAYagAAAAEHjFU7PNJLLoHEokVux2vyJtpz8gyHfU6EnQQ46PvnsIoWEyCtsTjPqMSAyjmYeyvw==", "5d00bf30-e2a2-447f-96d6-11aedfa4820f", "111111", "brok" });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "tbUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "54c9bb4b-1693-4818-8a71-fb9b9c825a8c", "cb6b5c8e-e9b4-4956-8149-aba76a6552f7" },
                    { "559ecd36-deaf-4c9b-882a-598830d69103", "cb6b5c8e-e9b4-4956-8149-aba76a6552f7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "24da78b22d2f409d80285c0f6aa0000c");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "24f1c8d29ec843578f9d44c0ecfd037d");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "5ad711dfe0284848bf67256c6fe24729");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "70e096c446dd48d181854a3d42f07ef1");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "b03322517ab94376b543df4e4ba36e1f");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "c297e2da90a74adbb15c2c050e715863");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "e37838f172f6449f880544f7ce97f60c");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "54c9bb4b-1693-4818-8a71-fb9b9c825a8c", "cb6b5c8e-e9b4-4956-8149-aba76a6552f7" });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "559ecd36-deaf-4c9b-882a-598830d69103", "cb6b5c8e-e9b4-4956-8149-aba76a6552f7" });

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbRoles",
                keyColumn: "Id",
                keyValue: "54c9bb4b-1693-4818-8a71-fb9b9c825a8c");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbRoles",
                keyColumn: "Id",
                keyValue: "559ecd36-deaf-4c9b-882a-598830d69103");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbUsers",
                keyColumn: "Id",
                keyValue: "cb6b5c8e-e9b4-4956-8149-aba76a6552f7");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "tbGenders",
                keyColumn: "Id",
                keyValue: "8322d0eabe3446e1adaa60e90bf1d9c8");
        }
    }
}
