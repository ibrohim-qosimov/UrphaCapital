using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.AuthServices
{
    public interface IAuthService
    {
        public string GenerateToken(Student user);
        public string GenerateToken(Admin user);
        public string GenerateToken(Mentor user);
    }
}
