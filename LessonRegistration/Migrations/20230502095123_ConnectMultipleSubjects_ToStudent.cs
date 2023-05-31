using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class ConnectMultipleSubjects_ToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Subjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Students_StudentId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subjects");
        }
    }
}
