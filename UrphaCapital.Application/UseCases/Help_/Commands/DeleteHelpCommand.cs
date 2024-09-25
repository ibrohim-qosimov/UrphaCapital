using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Help_.Commands
{
    public class DeleteHelpCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
