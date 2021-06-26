using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GameWeb.Migrations
{
    public partial class SeedRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Requirement",
                columns: new[] { "Id", "CPU", "DriveSize", "GPU", "GameId", "OS", "RAM" },
                values: new object[,]
                {
                    { 1, "test", 8, "test", null, "Windows", 8 },
                    { 2, "test", 8, "test", null, "Windows", 8 },
                    { 3, "test", 8, "test", null, "Windows", 8 },
                    { 4, "test", 8, "test", null, "Windows", 8 },
                    { 5, "test", 8, "test", null, "Windows", 8 },
                    { 6, "test", 8, "test", null, "Windows", 8 }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "MinimalRequirementsId", "Name", "Platform", "Publisher", "RecommendedRequirementsId", "ReleaseDate" },
                values: new object[] { 1, "Lorem ipsum", "Mojang", "Sandbox", "1.jpg", 1, "Minecraft", "PC, Xbox, PlayStation, Android", "Mojang", 2, new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "MinimalRequirementsId", "Name", "Platform", "Publisher", "RecommendedRequirementsId", "ReleaseDate" },
                values: new object[] { 2, "Lorem ipsum", "Ubisoft", "Platformówki", "2.jpg", 3, "Rayman 3: Hoodlum Havoc", "PC, Xbox, PlayStation", "Ubisoft", 4, new DateTime(2003, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "MinimalRequirementsId", "Name", "Platform", "Publisher", "RecommendedRequirementsId", "ReleaseDate" },
                values: new object[] { 3, "Lorem ipsum", "Bugbear Entertainment", "Wyścigi", "3.jpg", 5, "FlatOut 2", "PC, Xbox, PlayStation", "Empire Interactive", 6, new DateTime(2006, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
