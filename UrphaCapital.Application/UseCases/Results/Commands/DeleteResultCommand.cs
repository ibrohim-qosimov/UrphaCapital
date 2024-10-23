using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Results.Commands
{
    public class DeleteResultCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
