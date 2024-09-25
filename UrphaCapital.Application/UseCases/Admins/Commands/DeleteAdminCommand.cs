using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Admins.Commands
{
    public class DeleteAdminCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
    }
}
