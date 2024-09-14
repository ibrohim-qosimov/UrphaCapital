using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities
{
    public class Homeworks
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FILE { get; set; }
        public string Description { get; set; }
        public long StudentId { get; set; }
        public int? Grade { get; set; }
        public long? MentorId { get; set; }
        public Guid LessonId { get; set; }

        public Lesson Lesson { get; set; }
    }
}
