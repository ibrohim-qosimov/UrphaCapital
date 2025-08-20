using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities
{
    public class UserCourse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CourseId { get; set; }
    }
}
