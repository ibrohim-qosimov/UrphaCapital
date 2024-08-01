using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Commands
{
    public class CreateLessonCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public long CourseId { get; set; }
        public IFormFile Video { get; set; }
    }
}
