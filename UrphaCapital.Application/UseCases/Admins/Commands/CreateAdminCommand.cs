using MediatR;
using UrphaCapital.Application.Filters;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Admins.Commands
{
    public class CreateAdminCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Password(minimumLength: 8)]
        public string PasswordHash { get; set; }
    }
}
