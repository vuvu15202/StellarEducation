using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_API.Migrations
{
    public partial class dbcontextver4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamCandidate",
                columns: table => new
                {
                    ExamCandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    candidateId = table.Column<int>(type: "int", nullable: false),
                    questionBankId = table.Column<int>(type: "int", nullable: false),
                    StartExamDate = table.Column<DateTime>(type: "date", nullable: true),
                    SubmitedDate = table.Column<DateTime>(type: "date", nullable: true),
                    SubmitedReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitedListening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitedWriting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitedSpeaking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bandScoreReading = table.Column<double>(type: "float", nullable: true),
                    bandScoreListening = table.Column<double>(type: "float", nullable: true),
                    bandScoreWriting = table.Column<double>(type: "float", nullable: true),
                    bandScoreSpeaking = table.Column<double>(type: "float", nullable: true),
                    overall = table.Column<double>(type: "float", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    isPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCandidate", x => x.ExamCandidateId);
                    table.ForeignKey(
                        name: "FK_ExamCandidate_QuestionBank",
                        column: x => x.questionBankId,
                        principalTable: "QuestionBank",
                        principalColumn: "questionBankId");
                    table.ForeignKey(
                        name: "FK_ExamCandidate_User",
                        column: x => x.candidateId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidate_candidateId",
                table: "ExamCandidate",
                column: "candidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidate_questionBankId",
                table: "ExamCandidate",
                column: "questionBankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamCandidate");
        }
    }
}
