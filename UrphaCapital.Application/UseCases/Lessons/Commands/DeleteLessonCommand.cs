using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Lessons.Commands
{
    public class DeleteLessonCommand : IRequest<ResponseModel>
    {
        public string Id { get; set; }
    }
}
