using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetAllStudentsByIdQuery : IRequest<Student>
    {
        public long Id { get; set; }
    }
}
