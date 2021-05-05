using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWeb.Migrations
{
    public partial class AddRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MinimalRequirementsId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedRequirementsId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    GPU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriveSize = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirement_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_MinimalRequirementsId",
                table: "Game",
                column: "MinimalRequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_RecommendedRequirementsId",
                table: "Game",
                column: "RecommendedRequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_GameId",
                table: "Requirement",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Requirement_MinimalRequirementsId",
                table: "Game",
                column: "MinimalRequirementsId",
                principalTable: "Requirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Requirement_RecommendedRequirementsId",
                table: "Game",
                column: "RecommendedRequirementsId",
                principalTable: "Requirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Requirement_MinimalRequirementsId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Requirement_RecommendedRequirementsId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropIndex(
                name: "IX_Game_MinimalRequirementsId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_RecommendedRequirementsId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MinimalRequirementsId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "RecommendedRequirementsId",
                table: "Game");

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 1, "Lorem ipsum", "Mojang", "Sandbox", "1.jpg", "Minecraft", "PC, Xbox, PlayStation, Android", "Mojang", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 2, "Lorem ipsum", "Ubisoft", "Platformówki", "2.jpg", "Rayman 3: Hoodlum Havoc", "PC, Xbox, PlayStation", "Ubisoft", new DateTime(2003, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Image", "Name", "Platform", "Publisher", "ReleaseDate" },
                values: new object[] { 3, "Lorem ipsum", "Bugbear Entertainment", "Wyścigi", "3.jpg", "FlatOut 2", "PC, Xbox, PlayStation", "Empire Interactive", new DateTime(2006, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
