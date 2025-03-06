using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_userlecturer",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_userlecturer",
                table: "Course",
                column: "LecturerID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_userlecturer",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_userlecturer",
                table: "Course",
                column: "LecturerID",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
