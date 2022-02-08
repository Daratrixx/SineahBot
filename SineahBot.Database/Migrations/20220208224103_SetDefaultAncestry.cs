using Microsoft.EntityFrameworkCore.Migrations;

namespace SineahBot.Database.Migrations
{
    public partial class SetDefaultAncestry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CharacterAncestry",
                table: "Characters",
                type: "TEXT",
                nullable: true,
                defaultValue: "Human",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.Sql(@"UPDATE Characters SET CharacterAncestry = ""Human"";");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CharacterAncestry",
                table: "Characters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValue: "Human");
        }
    }
}
