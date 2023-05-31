using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class ConnectInstitueToDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstituteId",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstituteId",
                table: "Departments",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Intitutes_InstituteId",
                table: "Departments",
                column: "InstituteId",
                principalTable: "Intitutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Intitutes_InstituteId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstituteId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InstituteId",
                table: "Departments");
        }
    }
}
