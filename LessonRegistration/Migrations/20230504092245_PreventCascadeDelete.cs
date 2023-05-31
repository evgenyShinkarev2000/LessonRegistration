using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class PreventCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Semesters_UsedInSemestersId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectsId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_RegisteredStudentsId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SelectedSubjectId",
                table: "StudentSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Semesters_UsedInSemestersId",
                table: "SemesterSubject",
                column: "UsedInSemestersId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectsId",
                table: "SemesterSubject",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_RegisteredStudentsId",
                table: "StudentSubject",
                column: "RegisteredStudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SelectedSubjectId",
                table: "StudentSubject",
                column: "SelectedSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Semesters_UsedInSemestersId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectsId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_RegisteredStudentsId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SelectedSubjectId",
                table: "StudentSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Departments_DepartmentId",
                table: "Semesters",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Semesters_UsedInSemestersId",
                table: "SemesterSubject",
                column: "UsedInSemestersId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectsId",
                table: "SemesterSubject",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Semesters_SemesterId",
                table: "Students",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_RegisteredStudentsId",
                table: "StudentSubject",
                column: "RegisteredStudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SelectedSubjectId",
                table: "StudentSubject",
                column: "SelectedSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
