using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class AddQuestionInCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Questions",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_LessonId",
                table: "Questions",
                newName: "IX_Questions_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Courses_CourseId",
                table: "Questions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Courses_CourseId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Questions",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_CourseId",
                table: "Questions",
                newName: "IX_Questions_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                table: "Questions",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
