using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Domain.Entities
{
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string Picture {  get; set; }
        public string Price { get; set; }
        public long MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }
}
