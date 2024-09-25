using MediatR;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Lessons.Queries
{
    public class GetLessonByIdQuery : IRequest<Lesson>
    {
        public string Id { get; set; }
    }
}
