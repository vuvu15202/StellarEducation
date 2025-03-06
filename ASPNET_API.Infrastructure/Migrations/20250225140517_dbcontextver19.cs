using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerID",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_LecturerID",
                table: "Course",
                column: "LecturerID");

            migrationBuilder.AddForeignKey(
                name: "fk_course_userlecturer",
                table: "Course",
                column: "LecturerID",
                principalTable: "User",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_userlecturer",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_LecturerID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LecturerID",
                table: "Course");
        }
    }
}
