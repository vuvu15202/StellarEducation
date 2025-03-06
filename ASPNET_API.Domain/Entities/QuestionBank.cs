using ASPNET_API.Domain.Entities.IELTS;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASPNET_API.Domain.Entities
{
    public class QuestionBank
    {
        public QuestionBank() {
            ExamCandidates = new HashSet<ExamCandidate>();
            Lessons = new HashSet<Lesson>();
        }
       
        public int QuestionBankId { get; set; }
        public string ExamCode { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        
        public bool IsClosed { get; set; } = false!;
        public int? LecturerId { get; set; }

        public string? Reading { get; set; } = "{\"Time\":\"3600\"}"!;
        public string? Listening { get; set; } = "{\"Time\":\"3600\"}"!;
        public string? Writing { get; set; } = "{\"Time\":\"3600\"}"!;
        public string? Speaking { get; set; } = "{\"Time\":\"3600\"}"!;

        public Reading? ReadingJSON
        {
            get
            {
                return Reading.Equals("{}") ? null : JsonSerializer.Deserialize<Reading>(Reading);
            }
           
        }

        public Listening? ListeningJSON
        {
            get
            {
                return Listening.Equals("{}") ? null : JsonSerializer.Deserialize<Listening>(Listening);
            }
            
        }
        public Writing? WritingJSON
        {
            get
            {
                return Writing.Equals("{}") ? null : JsonSerializer.Deserialize<Writing>(Writing);
            }
            
        }

        public bool IsDelete { get; set; }
        public bool IsPrivate { get; set; } = false!;

        public virtual User? Lecturer { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<ExamCandidate>? ExamCandidates { get; set; }

        [JsonIgnore]
        public virtual ICollection<Lesson>? Lessons { get; set; }
    }
}
