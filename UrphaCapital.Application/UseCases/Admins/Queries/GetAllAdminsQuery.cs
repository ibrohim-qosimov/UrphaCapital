using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Admins.Queries
{
    public class GetAllAdminsQuery : IRequest<IEnumerable<Admin>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
