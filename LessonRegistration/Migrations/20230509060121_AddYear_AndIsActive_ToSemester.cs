using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddYear_AndIsActive_ToSemester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Semesters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Semesters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Semesters");
        }
    }
}
