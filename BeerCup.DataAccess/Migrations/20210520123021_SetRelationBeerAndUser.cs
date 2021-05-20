using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class SetRelationBeerAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Users_UserId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_UserId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Battles");

            migrationBuilder.CreateTable(
                name: "BeerUser",
                columns: table => new
                {
                    VotedBeersId = table.Column<int>(type: "int", nullable: false),
                    VotingUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerUser", x => new { x.VotedBeersId, x.VotingUsersId });
                    table.ForeignKey(
                        name: "FK_BeerUser_Beers_VotedBeersId",
                        column: x => x.VotedBeersId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerUser_Users_VotingUsersId",
                        column: x => x.VotingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerUser_VotingUsersId",
                table: "BeerUser",
                column: "VotingUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Battles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_UserId",
                table: "Battles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Users_UserId",
                table: "Battles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
