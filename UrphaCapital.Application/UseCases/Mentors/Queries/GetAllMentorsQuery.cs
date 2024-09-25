using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Queries
{
    public class GetAllMentorsQuery : IRequest<IEnumerable<Mentor>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
