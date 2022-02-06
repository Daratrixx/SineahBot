using Microsoft.EntityFrameworkCore.Migrations;

namespace SineahBot.Database.Migrations
{
    public partial class AddCharacterAncestry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterAncestry",
                table: "Characters",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterAncestry",
                table: "Characters");
        }
    }
}
