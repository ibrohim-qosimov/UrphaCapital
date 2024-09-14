using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.DTOs
{
    public class HomeworkResultView
    {
        public long HomeworkId { get; set; }
        public string LessonTitle { get; set; }
        public string FILE {  get; set; }
        public int? Grade { get; set; }
    }
}
