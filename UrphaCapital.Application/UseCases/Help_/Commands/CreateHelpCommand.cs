using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Help_.Commands
{
    public class CreateHelpCommand : IRequest<ResponseModel>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
