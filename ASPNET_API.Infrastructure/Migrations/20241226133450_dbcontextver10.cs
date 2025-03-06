using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lecture",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserRoleId",
                table: "Course");
        }
    }
}
