using MediatR;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Selects.Queries
{
    public class SelectAllStudentsQuery : IRequest<IEnumerable<StudentSelectModel>>
    {
    }
}
