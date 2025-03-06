
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASPNET_API.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Reviews = new HashSet<Review>();
            Notifications = new HashSet<Notification>();
            ExamCandidates = new HashSet<ExamCandidate>();
            ResolvedConsultationRequests = new HashSet<ConsultationRequest>();
			Courses = new HashSet<Course>();

		}

		public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!; 
        [JsonIgnore]
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; } 
        public DateTime? EnrollDate { get; set; }
		public string? Image { get; set; }
		public string? Description { get; set; }



		[JsonIgnore]
        public virtual ICollection<UserRole>? UserRoles { get; set; }
		[JsonIgnore]
		public virtual ICollection<Course>? Courses { get; set; }
		[JsonIgnore]
        public virtual ICollection<Review>? Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Notification>? Notifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<QuestionBank>? QuestionBanks { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExamCandidate>? ExamCandidates { get; set; }
        [JsonIgnore]
        public virtual ICollection<ConsultationRequest>? ResolvedConsultationRequests { get; set; }
    }
}
