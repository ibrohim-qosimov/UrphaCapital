using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Queries
{
    public class GetAdminByIdQuery : IRequest<Admin>
    {
        public long Id { get; set; }
    }
}
