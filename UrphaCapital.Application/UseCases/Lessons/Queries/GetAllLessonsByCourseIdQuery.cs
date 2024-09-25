using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Lessons.Queries
{
    public class GetAllLessonsByCourseIdQuery : IRequest<IEnumerable<Lesson>>
    {
        public string CourseId { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
