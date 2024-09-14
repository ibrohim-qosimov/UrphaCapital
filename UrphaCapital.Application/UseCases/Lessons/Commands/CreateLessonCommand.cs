using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Commands
{
    public class CreateLessonCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public string HomeworkDescription { get; set; }
        public IFormFile Video { get; set; }
    }
}
