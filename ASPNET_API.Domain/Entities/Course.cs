using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASPNET_API.Domain.Entities
{
    public partial class Course
    {
        public Course()
        {
            CourseEnrolls = new HashSet<CourseEnroll>();
            Lessons = new HashSet<Lesson>();
            Reviews = new HashSet<Review>();
        }

        public int CourseId { get; set; }
        public int CategoryId { get; set; }
		public int? LecturerId { get; set; } = null;
		public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;
        public bool IsDelete { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
		public string? Language { get; set; } = "Vietnamese";

		[JsonIgnore]
        public virtual Category? Category { get; set; } = null!;
		[JsonIgnore]
		public virtual User? Lecturer { get; set; } = null!;
		[JsonIgnore]
        public virtual ICollection<Lesson>? Lessons { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Review>? Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseEnroll>? CourseEnrolls { get; set; }
    }
}
