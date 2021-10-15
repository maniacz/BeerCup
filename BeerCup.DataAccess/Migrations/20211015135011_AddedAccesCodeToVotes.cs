using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedAccesCodeToVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessCodeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccessCodes",
                columns: table => new
                {
                    AccessCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessCodes", x => x.AccessCodeId);
                });

            migrationBuilder.InsertData(
                table: "AccessCodes",
                columns: new[] { "AccessCodeId", "Code" },
                values: new object[,]
                {
                    { 1, "A001" },
                    { 2, "A002" },
                    { 3, "A003" },
                    { 4, "A004" },
                    { 5, "V001" },
                    { 6, "V002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccessCodeId",
                table: "Users",
                column: "AccessCodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AccessCodes_AccessCodeId",
                table: "Users",
                column: "AccessCodeId",
                principalTable: "AccessCodes",
                principalColumn: "AccessCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AccessCodes_AccessCodeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccessCodes");

            migrationBuilder.DropIndex(
                name: "IX_Users_AccessCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessCodeId",
                table: "Users");
        }
    }
}
