using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedDataToBattleRoutings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BattleRoutings",
                columns: new[] { "Id", "FromBattleNo", "IsSecondBattle", "ToBattleNo" },
                values: new object[,]
                {
                    { 1, 1, false, 9 },
                    { 2, 2, true, 9 },
                    { 3, 3, false, 10 },
                    { 4, 4, true, 10 },
                    { 5, 5, false, 11 },
                    { 6, 6, true, 11 },
                    { 7, 7, false, 12 },
                    { 8, 8, true, 12 },
                    { 9, 9, false, 13 },
                    { 10, 10, true, 13 },
                    { 11, 11, false, 14 },
                    { 12, 12, true, 14 },
                    { 13, 13, false, 15 },
                    { 14, 14, true, 15 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BattleRoutings",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
