using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedBattlePlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BattlePlaceId",
                table: "Battles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BattlePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattlePlaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_BattlePlaceId",
                table: "Battles",
                column: "BattlePlaceId",
                unique: true,
                filter: "[BattlePlaceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_BattlePlaces_BattlePlaceId",
                table: "Battles",
                column: "BattlePlaceId",
                principalTable: "BattlePlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_BattlePlaces_BattlePlaceId",
                table: "Battles");

            migrationBuilder.DropTable(
                name: "BattlePlaces");

            migrationBuilder.DropIndex(
                name: "IX_Battles_BattlePlaceId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "BattlePlaceId",
                table: "Battles");
        }
    }
}
