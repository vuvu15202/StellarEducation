using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dncontextver7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersListening",
                table: "ExamCandidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersReading",
                table: "ExamCandidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersSpeaking",
                table: "ExamCandidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersWriting",
                table: "ExamCandidate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswersListening",
                table: "ExamCandidate");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersReading",
                table: "ExamCandidate");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersSpeaking",
                table: "ExamCandidate");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersWriting",
                table: "ExamCandidate");
        }
    }
}
