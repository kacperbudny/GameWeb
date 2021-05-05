using Microsoft.EntityFrameworkCore.Migrations;

namespace GameWeb.Migrations
{
    public partial class ReplaceTestSeedDataWithRealRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "RAM",
                table: "Requirement",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Intel Core i3 3210 / AMD A8 7600 APU", 180, "NVIDIA GeForce 400 Series / AMD Radeon HD 7000 series", "64-bit Windows 7", 4.0 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Intel Core i5 4690 / AMD A10 7800", 4000, "NVIDIA GeForce 700 Series / AMD Radeon Rx 200 Series", "64-bit Windows 10", 8.0 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Pentium III 600 MHz / AMD Athlon 600 MHz", 800, "Kompatybilna z DirectX 8.1", "Windows XP", 0.25600000000000001 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Pentium III 1 GHz / AMD Athlon 1 Ghz", 800, "Kompatybilna z DirectX 9", "Windows XP", 0.51200000000000001 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Intel Pentium 4 2.0GHz / AMD Athlon XP 2000+", 3500, "AMD Radeon HD 4350 / NVIDIA GeForce 6600 GT", "32-bit Windows XP", 0.25600000000000001 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "Intel Pentium 4 2.0GHz / AMD Athlon XP 2500+", 4000, "AMD Radeon 9600 Series / NVIDIA GeForce 6600", "32-bit Windows XP", 0.51200000000000001 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RAM",
                table: "Requirement",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });

            migrationBuilder.UpdateData(
                table: "Requirement",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CPU", "DriveSize", "GPU", "OS", "RAM" },
                values: new object[] { "test", 8, "test", "Windows", 8 });
        }
    }
}
