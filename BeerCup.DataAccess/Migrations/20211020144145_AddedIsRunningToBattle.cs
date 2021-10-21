using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class AddedIsRunningToBattle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRunning",
                table: "Battles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRunning",
                table: "Battles");
        }
    }
}
