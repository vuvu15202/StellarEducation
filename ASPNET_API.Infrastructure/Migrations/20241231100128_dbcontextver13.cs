using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionBankId",
                table: "Lesson",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_QuestionBankId",
                table: "Lesson",
                column: "QuestionBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_QuestionBank",
                table: "Lesson",
                column: "QuestionBankId",
                principalTable: "QuestionBank",
                principalColumn: "questionBankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_QuestionBank",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_QuestionBankId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "QuestionBankId",
                table: "Lesson");
        }
    }
}
