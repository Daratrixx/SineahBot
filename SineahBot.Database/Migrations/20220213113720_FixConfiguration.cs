using Microsoft.EntityFrameworkCore.Migrations;

namespace SineahBot.Database.Migrations
{
    public partial class FixConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Characters_IdCharacter",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Players_Characters_IdCharacter",
                table: "Players",
                column: "IdCharacter",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
