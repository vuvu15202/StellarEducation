using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASPNET_MVC.temp
{
    public partial class Lesson
    {

        public int LessonId { get; set; }
        public int? LessonNum { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? Quiz { get; set; }
        public int? PreviousLessioNum { get; set; }
        public bool IsDelete { get; set; }


        //[JsonIgnore]
        //public virtual Lesson? PreviousLession { get; set; }
        [JsonIgnore]
        public virtual Course? Course { get; set; } = null!;
    }
}
