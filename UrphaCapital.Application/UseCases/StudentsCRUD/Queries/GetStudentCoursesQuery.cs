using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetStudentCoursesQuery : IRequest<IEnumerable<Course>>
    {
        public long Id { get; set; }
    }
}
