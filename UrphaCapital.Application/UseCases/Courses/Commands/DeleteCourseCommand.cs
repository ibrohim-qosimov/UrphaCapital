using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<ResponseModel>
    {
        public int Id { get; set; }
    }
}
