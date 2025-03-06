using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.DTOs
{
    public class LessonModel
    {
        public int LessonId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập số của bài giảng.")]
        public int LessonNum { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn khóa học.")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên bài giảng.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập video URL.")]
        public string VideoUrl { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập video Time.")]
        public int VideoTime { get; set; } = 120;
        public string? Quiz { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số của bài giảng tiên quyết.")]
        public int PreviousLessioNum { get; set; } = 1;
        public bool IsDelete { get; set; } = false;
    }
}
