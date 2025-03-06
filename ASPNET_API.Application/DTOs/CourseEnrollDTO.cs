namespace ASPNET_API.Application.DTOs
{
    public class CourseEnrollDTO
    {
        public int CourseEnrollId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public int LessonCurrent { get; set; } = 1;
        public int CourseStatus { get; set; }
        public string? Grade { get; set; }
        public float? AverageGrade { get; set; }
        public string? Quiz { get; set; }
        public string? StudentFeeId { get; set; }
        public virtual CourseDTO Course { get; set; } = null!;
        public virtual UserDTO User { get; set; } = null!;

    }
}
