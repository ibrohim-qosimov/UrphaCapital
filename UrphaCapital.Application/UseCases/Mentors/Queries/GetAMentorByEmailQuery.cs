using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Mentors.Queries
{
    public class GetAMentorByEmailQuery : IRequest<Mentor>
    {
        public string Email { get; set; }
    }
}
