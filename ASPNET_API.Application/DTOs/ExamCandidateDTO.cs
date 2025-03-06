using ASPNET_API.Application.DTOs.IELTS;
using System.Text.Json.Serialization;
namespace ASPNET_API.Application.DTOs
{
    public class ExamCandidateDTO
    {

        public int ExamCandidateId { get; set; }

        public int CandidateId { get; set; }
        public int QuestionBankId { get; set; }
        public string? TypeExam { get; set; } = "quiz";


        public DateTime? StartExamDate { get; set; }
        public DateTime? SubmitedDate { get; set; }

        public string? SubmitedReading { get; set; }
        public string? SubmitedListening { get; set; }
        public string? SubmitedWriting { get; set; }
        public string? SubmitedSpeaking { get; set; }

        public double? BandScoreReading { get; set; }
        public double? BandScoreListening { get; set; }
        public double? BandScoreWriting { get; set; }
        public double? BandScoreSpeaking { get; set; }
        public double? Overall { get; set; }

        public bool IsDelete { get; set; } = false!;
        public bool IsComplete { get; set; } = false!;

        public UserDTO? Candidate { get; set; } = null!;
        public QuestionBankDTO? QuestionBank { get; set; } = null!;

    }
}
