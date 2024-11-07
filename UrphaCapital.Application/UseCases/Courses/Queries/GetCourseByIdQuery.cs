using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<Course>
    {
        public int Id { get; set; }
    }
}
