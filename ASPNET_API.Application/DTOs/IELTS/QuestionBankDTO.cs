using ASPNET_API.Domain.Entities;
using ASPNET_API.Domain.Entities.IELTS;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASPNET_API.Application.DTOs.IELTS
{
    public class QuestionBankDTO
    {
        public int QuestionBankId { get; set; }
        public string ExamCode { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }
        public int? LecturerId { get; set; }

        public bool IsDelete { get; set; }

        public virtual User? Lecturer { get; set; } = null!;

        [JsonIgnore]
        public string? Reading { get; set; } = "{\"Time\":\"60\"}"!; 

        [JsonIgnore]
        public string? Listening { get; set; } = "{\"Time\":\"60\"}"!;

        [JsonIgnore]
        public string? Writing { get; set; } = "{\"Time\":\"60\"}"!;

        [JsonIgnore]
        public string? Speaking { get; set; } = "{\"Time\":\"60\"}"!;

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

    }
}


