using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWeb.Migrations
{
    public partial class AddOptionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Requirement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GameCommentThread",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecommendedRequirementsId",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MinimalRequirementsId",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 1,
                column: "GameId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 2,
                column: "GameId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 3,
                column: "GameId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 4,
                column: "GameId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 5,
                column: "GameId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 6,
                column: "GameId",
                value: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Requirement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GameCommentThread",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RecommendedRequirementsId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MinimalRequirementsId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 1,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 2,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 3,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 4,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 5,
                column: "GameId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 6,
                column: "GameId",
                value: null);
        }
    }
}
