using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quiz",
                table: "CourseEnroll",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quiz",
                table: "CourseEnroll");
        }
    }
}
