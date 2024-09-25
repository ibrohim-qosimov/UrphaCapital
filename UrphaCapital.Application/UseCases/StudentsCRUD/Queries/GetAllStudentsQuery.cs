using MediatR;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.StudentsCRUD.Queries
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<Student>>
    {
        public int Index { get; set; }
        public int Count { get; set; }
    }
}
