using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities.Auth
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public List<Course>? Courses { get; set; }

        public EUserRole Role { get; set; }
    }

    public enum EUserRole
    {
        Student = 0,
        Mentor = 1,
        Admin = 3
    }
}
