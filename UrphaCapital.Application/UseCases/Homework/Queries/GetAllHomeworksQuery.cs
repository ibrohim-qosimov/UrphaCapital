using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Homework.Queries
{
    public class GetAllHomeworksQuery : IRequest<IEnumerable<Homeworks>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
