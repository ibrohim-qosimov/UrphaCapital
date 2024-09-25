using UrphaCapital.Application.ViewModels;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.ExternalServices.AuthServices
{
    public interface IAuthService
    {
        public TokenModel GenerateToken(Student user);
        public TokenModel GenerateToken(Admin user);
        public TokenModel GenerateToken(Mentor user);
    }
}
