using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWeb.Migrations
{
    public partial class AddRelationBetweenCommentAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GameComment");

            migrationBuilder.AddColumn<string>(
                name: "AuthorID",
                table: "GameComment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameComment_AuthorID",
                table: "GameComment",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameComment_AspNetUsers_AuthorID",
                table: "GameComment",
                column: "AuthorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameComment_AspNetUsers_AuthorID",
                table: "GameComment");

            migrationBuilder.DropIndex(
                name: "IX_GameComment_AuthorID",
                table: "GameComment");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "GameComment");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GameComment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
