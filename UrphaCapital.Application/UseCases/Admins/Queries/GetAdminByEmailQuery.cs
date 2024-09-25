using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Queries
{
    public class GetAdminByEmailQuery : IRequest<Admin?>
    {
        public string Email { get; set; }
    }
}
