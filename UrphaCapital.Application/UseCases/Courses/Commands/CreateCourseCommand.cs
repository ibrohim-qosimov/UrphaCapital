using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Commands
{
    public class CreateCourseCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public IFormFile Picture { get; set; }
        public decimal Price { get; set; }
        public long MentorId { get; set; }
    }
}
