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
        public long LessonId { get; set; }
        public long studentId {  get; set; }
        public Lesson Lesson { get; set; }
    }
}
