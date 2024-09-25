using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Commands
{
    public class DeleteStudentCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
