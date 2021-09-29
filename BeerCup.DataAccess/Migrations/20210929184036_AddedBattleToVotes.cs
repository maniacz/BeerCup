using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedBattleToVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BattleId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "Votes");
        }
    }
}
