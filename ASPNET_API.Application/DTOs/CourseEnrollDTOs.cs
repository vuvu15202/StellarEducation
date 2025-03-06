using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.DTOs
{
    public class CourseEnrollDTOs
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        public string EnrollDate { get; set; }
        public string EndDate { get; set; }
        public int CourseStatus { get; set; }
        public string? Grade { get; set; }
        public float? AverageGrade { get; set; }
        public string? StudentFeeId { get; set; }
        public List<ExamCandidate> examCandidates { get; set; }
    }
}
