using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class ConnectSemeter_ToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Students",
                newName: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SemesterId",
                table: "Students",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SemesterId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "Students",
                newName: "Course");
        }
    }
}
