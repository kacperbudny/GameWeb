using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWeb.Migrations
{
    public partial class AddLongerLoremIpsumGameDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ut mattis dolor. Curabitur accumsan sapien quam, vel volutpat lacus euismod eget. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eget ornare quam, nec lobortis nunc. Ut dui ex, faucibus ut finibus dictum, pretium in elit. Aliquam ac molestie sem. Morbi dictum nisl in justo venenatis, id ultricies elit porta.");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Aliquam molestie mollis sagittis. Curabitur a nibh eu tortor dapibus sodales. Proin a aliquet turpis. Proin augue massa, posuere ac interdum at, aliquam sed sem. Donec felis turpis, viverra eget elit ut, facilisis auctor risus. Donec aliquam augue urna, et accumsan nisi lobortis in. Ut sagittis nunc feugiat nibh venenatis tempor. Sed rutrum ullamcorper sapien et eleifend. \n\nInteger et odio tincidunt, luctus felis non, eleifend libero. Etiam a vulputate enim. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In ac ante iaculis, scelerisque orci varius, ornare ligula. Donec nec faucibus eros. Phasellus lacinia elit arcu, condimentum sodales libero pretium sed. In ut commodo orci. Sed sagittis viverra ante, id dignissim metus mollis ut.");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Donec vehicula ornare elit, nec tempor ante ornare a. In hac habitasse platea dictumst. Etiam rhoncus ornare vestibulum. Quisque id odio pellentesque, maximus massa in, aliquam quam. Donec lacus sem, lobortis a dolor vel, condimentum pellentesque orci. Aenean nunc ipsum, cursus a blandit at, pellentesque id purus. Quisque tincidunt urna vel sem maximus, a efficitur urna suscipit.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Lorem ipsum");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Lorem ipsum");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Lorem ipsum");
        }
    }
}
