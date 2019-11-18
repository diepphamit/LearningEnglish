using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class ChangeAchievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Lessons_LessonId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_LessonId",
                table: "Achievements");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Achievements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CourseId",
                table: "Achievements",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_CourseId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Achievements");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_LessonId",
                table: "Achievements",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Lessons_LessonId",
                table: "Achievements",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
