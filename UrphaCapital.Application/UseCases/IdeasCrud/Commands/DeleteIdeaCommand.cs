using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.IdeasCrud.Commands
{
    public class DeleteIdeaCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
