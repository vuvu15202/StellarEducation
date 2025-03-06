using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASPNET_API.Application.DTOs
{

    public partial class CourseDTO
    {

        public int CourseId { get; set; }
        public int CategoryId { get; set; }
		public int LecturerId { get; set; }
		public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;
        public int? TotalTime { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
		public string? UpdatedAtString { get; set; }
		public string? Language { get; set; } = "Vietnamese";

		public UserDTO? Lecturer { get; set; } = null;
		public int NumberEnrolled { get; set; }
        public List<LessonDTO>? Lessons { get; set; }
        public Category? Category { get; set; } = null!;

        public int? TotalStudent { get; set; } = null!;
        public int? TotalStudentFee { get; set; } = null!;
        public virtual List<StudentFee>? StudentFees { get; set; } = null!;


	}
}
