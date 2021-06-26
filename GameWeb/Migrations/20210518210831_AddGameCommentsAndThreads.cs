using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GameWeb.Migrations
{
    public partial class AddGameCommentsAndThreads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.CreateTable(
                name: "GameCommentThread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCommentThread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCommentThread_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Body = table.Column<string>(type: "ntext", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameComment_GameCommentThread_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "GameCommentThread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameComment_ThreadId",
                table: "GameComment",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCommentThread_GameId",
                table: "GameCommentThread",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameComment");

            migrationBuilder.DropTable(
                name: "GameCommentThread");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostID = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "ntext", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                });
        }
    }
}
