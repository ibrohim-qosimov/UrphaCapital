using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities.Auth
{
    public class Mentor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public List<Course> Courses { get; set; }
    }
}
