using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Homework.Commands
{
    public class DeleteHomeworkCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
