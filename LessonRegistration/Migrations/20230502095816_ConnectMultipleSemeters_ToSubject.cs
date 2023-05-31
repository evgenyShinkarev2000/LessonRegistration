using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class ConnectMultipleSemeters_ToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemesterId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SemesterId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SemesterSubject",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "integer", nullable: false),
                    UsedInSemestersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubject", x => new { x.SubjectsId, x.UsedInSemestersId });
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Semesters_UsedInSemestersId",
                        column: x => x.UsedInSemestersId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_UsedInSemestersId",
                table: "SemesterSubject",
                column: "UsedInSemestersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterSubject");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Subjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SemesterId",
                table: "Subjects",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemesterId",
                table: "Subjects",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");
        }
    }
}
