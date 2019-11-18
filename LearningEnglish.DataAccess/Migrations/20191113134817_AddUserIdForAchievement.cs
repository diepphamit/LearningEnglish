using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class AddUserIdForAchievement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Achievements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Achievements");
        }
    }
}
