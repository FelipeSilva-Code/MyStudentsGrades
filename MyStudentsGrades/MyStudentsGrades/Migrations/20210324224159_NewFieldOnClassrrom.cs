using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStudentsGrades.Migrations
{
    public partial class NewFieldOnClassrrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolSubject",
                table: "Classroom",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolSubject",
                table: "Classroom");
        }
    }
}
