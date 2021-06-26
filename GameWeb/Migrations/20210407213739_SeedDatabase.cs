using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GameWeb.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 1, "Lorem ipsum", "Sandbox", "1.jpg", "Minecraft", "PC, Xbox, PlayStation, Android", "Mojang", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 2, "Lorem ipsum", "Platformówki", "1.jpg", "Rayman 3: Hoodlum Havoc", "PC, Xbox, PlayStation", "Ubisoft", new DateTime(2003, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 3, "Lorem ipsum", "Wyścigi", "3.jpg", "FlatOut 2", "PC, Xbox, PlayStation", "Empire Interactive", new DateTime(2006, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });
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
        }
    }
}
