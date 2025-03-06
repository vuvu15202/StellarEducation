using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_API.Domain.Entities.IELTS
{
    public class Writing
    {
        public string Time { get; set; }
        public List<Part>? Parts { get; set; }
    }
}
