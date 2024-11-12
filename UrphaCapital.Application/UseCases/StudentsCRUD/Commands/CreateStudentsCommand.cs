using MediatR;
using UrphaCapital.Application.Filters;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Commands
{
    public class CreateStudentsCommand : IRequest<ResponseModel>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Password(minimumLength: 8)]
        public string PasswordHash { get; set; }
    }
}
