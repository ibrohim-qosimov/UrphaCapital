using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Commands
{
    public class CreateHomeworkCommand : IRequest<ResponseModel>
    {
        public string Title { get; set; }
        public IFormFile FILE { get; set; }
        public string Description { get; set; }
        public long StudentId { get; set; }
        public Guid LessonId { get; set; }
    }
}
