using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Domain.Entities.IELTS
{
    public class Group
    {
        public string GroupNo { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string QuestionRange { get; set; }
        public string? FileURL { get; set; }
        public string? FileType { get; set; }

        public List<Question> Questions { get; set; }
    }
}
