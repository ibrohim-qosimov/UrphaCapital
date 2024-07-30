using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities
{
    public class Lesson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CourseId {  get; set; }
        public Course Course { get; set; }
        public string Video {  get; set; }
    }
}
