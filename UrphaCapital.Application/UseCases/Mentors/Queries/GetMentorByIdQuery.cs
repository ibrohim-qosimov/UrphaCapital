using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Queries
{
    public class GetMentorByIdQuery : IRequest<Mentor>
    {
        public long Id { get; set; }
    }
}
