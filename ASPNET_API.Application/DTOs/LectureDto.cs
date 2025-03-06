using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Application.DTOs
{
    public class LectureDto
    {
        public int LecturerId { get; set; }
        public string UserName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }


    }
}
