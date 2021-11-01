using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerCup.DataAccess.Migrations
{
    public partial class ExtendedFieldBattleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BattleName",
                table: "Battles",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "DefaultName",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "DefaultName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BattleName",
                table: "Battles",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "DefaultName",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldDefaultValue: "DefaultName");
        }
    }
}
