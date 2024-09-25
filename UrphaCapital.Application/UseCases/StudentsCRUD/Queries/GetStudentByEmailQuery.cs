using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetStudentByEmailQuery : IRequest<Student>
    {
        public string Email { get; set; }
    }
}
