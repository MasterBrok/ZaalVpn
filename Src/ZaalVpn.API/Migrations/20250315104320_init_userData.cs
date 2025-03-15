using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZaalVpn.API.Migrations
{
    /// <inheritdoc />
    public partial class init_userData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vpn");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "tbCountries",
                schema: "vpn",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbGenders",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbGenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbRoles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbServers",
                schema: "vpn",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbServers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbServers_tbCountries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "vpn",
                        principalTable: "tbCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUsers",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastOnlineAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShortId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_tbUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbUsers_tbGenders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "auth",
                        principalTable: "tbGenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbRoleClaims",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbRoleClaims_tbRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "tbRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbConfigs",
                schema: "vpn",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ServerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbConfigs_tbServers_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "vpn",
                        principalTable: "tbServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUserClaim",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbUserClaim_tbUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUserLogins",
                schema: "auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_tbUserLogins_tbUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUserRoles",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tbUserRoles_tbRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "tbRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbUserRoles_tbUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUserTokens",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_tbUserTokens_tbUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_tbConfigs_ServerId",
                schema: "vpn",
                table: "tbConfigs",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbRoleClaims_RoleId",
                schema: "auth",
                table: "tbRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "auth",
                table: "tbRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbServers_CountryId",
                schema: "vpn",
                table: "tbServers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbUserClaim_UserId",
                schema: "auth",
                table: "tbUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbUserLogins_UserId",
                schema: "auth",
                table: "tbUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbUserRoles_RoleId",
                schema: "auth",
                table: "tbUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "auth",
                table: "tbUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_GenderId",
                schema: "auth",
                table: "tbUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "auth",
                table: "tbUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbConfigs",
                schema: "vpn");

            migrationBuilder.DropTable(
                name: "tbRoleClaims",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbUserClaim",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbUserLogins",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbUserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbUserTokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbServers",
                schema: "vpn");

            migrationBuilder.DropTable(
                name: "tbRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbUsers",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "tbCountries",
                schema: "vpn");

            migrationBuilder.DropTable(
                name: "tbGenders",
                schema: "auth");
        }
    }
}
