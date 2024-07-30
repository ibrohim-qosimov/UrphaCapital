using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities
{
    public class Test
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public long LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
