using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.DTOs
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn danh mục.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên khóa học.")]
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; } = null!;
        [Required(ErrorMessage = "Bạn chưa nhập mô tả.")]
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; } = true;
        public long? Price { get; set; } = 0;
        public bool IsDelete { get; set; } = false;

        [Required(ErrorMessage = "Bạn chưa chọn giảng viên.")]
        public int LecturerId { get; set; }

    }
}
