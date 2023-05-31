using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class FixInstitutesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Intitutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Intitutes_InstituteId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Intitutes",
                table: "Intitutes");

            migrationBuilder.RenameTable(
                name: "Intitutes",
                newName: "Institutes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutes",
                table: "Institutes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Institutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Institutes_InstituteId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutes",
                table: "Institutes");

            migrationBuilder.RenameTable(
                name: "Institutes",
                newName: "Intitutes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Intitutes",
                table: "Intitutes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Intitutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Intitutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Intitutes_InstituteId",
                table: "Students",
                column: "InstituteId",
                principalTable: "Intitutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
