using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Commands
{
    public class UpdateHomeworkCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public IFormFile? FILE { get; set; }
        public string? Description { get; set; }
        public long? StudentId { get; set; }
        public Guid? LessonId { get; set; }
    }
}
