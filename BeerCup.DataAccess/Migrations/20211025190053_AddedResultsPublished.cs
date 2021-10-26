using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedResultsPublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BattlePlaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "ResultsPublished",
                table: "Battles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultsPublished",
                table: "Battles");

            migrationBuilder.InsertData(
                table: "BattlePlaces",
                columns: new[] { "Id", "Latitude", "Longitude" },
                values: new object[] { 1, 0.0, 0.0 });
        }
    }
}
