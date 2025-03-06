using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPrivate",
                table: "ExamCandidate");

            migrationBuilder.AddColumn<bool>(
                name: "isPrivate",
                table: "QuestionBank",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPrivate",
                table: "QuestionBank");

            migrationBuilder.AddColumn<bool>(
                name: "isPrivate",
                table: "ExamCandidate",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
