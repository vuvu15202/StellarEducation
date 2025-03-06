using ASPNET_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNET_API.Domain.Entities
{
    public partial class Lesson
    {

        public int LessonId { get; set; }
        public int? LessonNum { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn khóa học.")]
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public int? VideoTime { get; set; } = 60;
        public string? Quiz { get; set; }
        public int? PreviousLessioNum { get; set; }
        public bool IsDelete { get; set; } = false;

        public int? QuestionBankId { get; set; }

        //[JsonIgnore]
        //public virtual Lesson? PreviousLession { get; set; }
        [JsonIgnore]
        public virtual Course? Course { get; set; } = null!;
        [JsonIgnore]
        public virtual QuestionBank? QuestionBank { get; set; } = null!;
    }
}
