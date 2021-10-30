using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedBattleNoAndBattleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BattleName",
                table: "Battles",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "DefaultName");

            migrationBuilder.AddColumn<int>(
                name: "BattleNo",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleName",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "BattleNo",
                table: "Battles");
        }
    }
}
