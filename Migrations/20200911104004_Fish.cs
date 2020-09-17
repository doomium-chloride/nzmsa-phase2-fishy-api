using Microsoft.EntityFrameworkCore.Migrations;

namespace FishyGame.Migrations
{
    public partial class Fish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "colour",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "colour",
                table: "Student");
        }
    }
}
