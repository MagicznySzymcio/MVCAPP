using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAPP.Migrations
{
    public partial class test_hashy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "2eIDQOzq52bN8+neK99ozNzVGWdG3O9gfeYaCkhDIQk=", "dh5t7igbmnY3iBwKXlDIMQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt", "Username" },
                values: new object[] { "HUftzfjHe1Zd04UA5fJum9qf4U6A/IoqRWZwC3rlGII=", "y8MUSe1OtzQkeTwSlOkTcQ==", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "fVjyV6H9ljRWRMunvObkqTLZcoyZaxewyrkVywR4pG8=", "GEDp/ff5TScRoPrW2qoT/g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "root", "root" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Salt", "Username" },
                values: new object[] { "test", "test", "test" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Salt" },
                values: new object[] { "jan", "test" });
        }
    }
}
