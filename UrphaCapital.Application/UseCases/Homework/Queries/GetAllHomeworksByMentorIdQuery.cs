using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Homework.Queries
{
    public class GetAllHomeworksByMentorIdQuery : IRequest<IEnumerable<Homeworks>>
    {
        public long MentorId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
