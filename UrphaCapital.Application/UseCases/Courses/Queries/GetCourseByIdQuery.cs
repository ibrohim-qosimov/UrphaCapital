using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<Course>
    {
        public string Id { get; set; }
    }
}
