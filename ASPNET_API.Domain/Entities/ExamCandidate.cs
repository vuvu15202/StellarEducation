using System.Text.Json.Serialization;

namespace ASPNET_API.Domain.Entities
{
    public class ExamCandidate
    {
        public int ExamCandidateId { get; set; }

        public int CandidateId { get; set; }
        public int QuestionBankId { get; set; }
        public string? TypeExam { get; set; } = "quiz";


        public DateTime? StartExamDate { get; set; }
        public DateTime? SubmitedDate { get; set; }

        public string? SubmitedReading { get; set; } = "[]";
        public string? SubmitedListening { get; set; } = "[]";
        public string? SubmitedWriting { get; set; } = "[]";
        public string? SubmitedSpeaking { get; set; } = "[]";

        public int? CorrectAnswersReading { get; set; } = 0;
        public int? CorrectAnswersListening { get; set; } = 0;
        public int? CorrectAnswersWriting { get; set; } = 0;
        public int? CorrectAnswersSpeaking { get; set; } = 0;

        public double? BandScoreReading { get; set; } = 0;
        public double? BandScoreListening { get; set; } = 0;
        public double? BandScoreWriting { get; set; } = 0;
        public double? BandScoreSpeaking { get; set; } = 0;
        public double? Overall { get; set; } = 0;

        public bool IsDelete { get; set; } = false!;
        public bool IsComplete { get; set; } = false!;

        public virtual User? Candidate { get; set; } = null!;
        public virtual QuestionBank? QuestionBank { get; set; } = null!;

    }
}
