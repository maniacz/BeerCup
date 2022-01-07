using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class LuckyVotersBattleIdIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LuckyVoters_BattleId",
                table: "LuckyVoters");

            migrationBuilder.CreateIndex(
                name: "IX_LuckyVoters_BattleId",
                table: "LuckyVoters",
                column: "BattleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LuckyVoters_BattleId",
                table: "LuckyVoters");

            migrationBuilder.CreateIndex(
                name: "IX_LuckyVoters_BattleId",
                table: "LuckyVoters",
                column: "BattleId");
        }
    }
}
