using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEnglish.DataAccess.Migrations
{
    public partial class ChangeAchievement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Achievements");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Achievements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Achievements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Achievements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Courses_CourseId",
                table: "Achievements",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
