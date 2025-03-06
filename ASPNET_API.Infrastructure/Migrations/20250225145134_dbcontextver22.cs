using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lecture",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserRoleId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "LecturerID",
                table: "Course",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_LecturerID",
                table: "Course",
                newName: "IX_Course_LecturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "Course",
                newName: "LecturerID");

            migrationBuilder.RenameIndex(
                name: "IX_Course_LecturerId",
                table: "Course",
                newName: "IX_Course_LecturerID");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserRoleId",
                table: "Course",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "fk_lecture",
                table: "Course",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "UserRoleId");
        }
    }
}
