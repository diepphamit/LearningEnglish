using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class AddnameForQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Questions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Questions");
        }
    }
}
