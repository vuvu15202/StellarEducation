using ASPNET_MVC.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASPNET_MVC.temp
{
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Reviews = new HashSet<Review>();
        }

        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;
        public bool IsDelete { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Lesson>? Lessons { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review>? Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseEnroll>? CourseEnrolls { get; set; }
    }
}
