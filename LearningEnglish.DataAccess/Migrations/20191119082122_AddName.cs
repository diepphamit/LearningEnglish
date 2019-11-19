using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class AddName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vocabularies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pronunciations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pronunciations");
        }
    }
}
