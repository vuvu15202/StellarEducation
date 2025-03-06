using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_userlecturer",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_lecturer",
                table: "Course",
                column: "LecturerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_lecturer",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "fk_course_userlecturer",
                table: "Course",
                column: "LecturerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
