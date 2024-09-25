using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Commands
{
    public class AddMyCourseCommand : IRequest<ResponseModel>
    {
        public long StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
