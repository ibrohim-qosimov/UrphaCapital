using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Queries
{
    public class GetAllCoursesQuery : IRequest<IEnumerable<Course>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
