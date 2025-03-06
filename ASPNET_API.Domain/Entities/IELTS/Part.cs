using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Domain.Entities.IELTS
{
    public class Part
    {
        public string PartNo { get; set; }
        public string FileURL { get; set; }
        public string? FileType { get; set; }
        public string? QuestionRange { get; set; }
        public string? Title { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
