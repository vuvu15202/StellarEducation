namespace ASPNET_API.Application.DTOs.IELTS
{

    public class SubmitedAnswersDTO
    {
        public int QuestionBankId { get; set; }
        public string ExamCode { get; set; } = null!;
        public string ForQuestion { get; set; } = null!;
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }

    public class Answer
    {
        public string QuestionNo { get; set; }  
        public string? SubmitedAnswer { get; set; } = string.Empty;
    }
}
