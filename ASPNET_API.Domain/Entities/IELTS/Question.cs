using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Domain.Entities.IELTS
{
    public class Question
    {
        public string QuestionNo { get; set; }
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public string? ExplainString { get; set; }

    }
}
