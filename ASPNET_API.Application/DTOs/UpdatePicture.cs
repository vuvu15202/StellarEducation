using Microsoft.AspNetCore.Http;

namespace ASPNET_API.Application.DTOs
{
    public class UpdatePicture
    {
        public int QuestionBankId { get; set; }
        public string PartNo { get; set; }
        public string? GroupNo { get; set; }
        public string? ForQuestion { get; set; }

        public IFormFile? FileUploads { get; set; }
    }

    public class UpdateQuestion
    {
        public int QuestionBankId { get; set; }
        public string? ForQuestion { get; set; }
        public string? Time { get; set; } 
        public IFormFile? FileUploads { get; set; }
        public IFormFile? FileAudio { get; set; }

    }
    public class UpdateCandidate
    {
        public int QuestionBankId { get; set; }
        public IFormFile? FileUploads { get; set; }
    }

    public class UpdateStudent
    {
        public int CourseId { get; set; }
        public IFormFile? FileUploads { get; set; }
    }
}
